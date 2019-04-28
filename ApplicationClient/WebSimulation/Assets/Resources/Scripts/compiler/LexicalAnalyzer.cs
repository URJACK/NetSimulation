using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 词法分析器
/// 词法分析器能够将一段原式文本进行代入，从而尝试将他们解析成为一个个的单词
/// 每一个单词都应该具有“位置”，“文本内容”，“词性”这三个属性
/// 因为在根据语法进行编译指令的时候，每一个单词都是一个最基本的单位。
/// 关于“位置”，当语句发生错误的时候，我们需要有一种方式能够准确的告知我们出错的位置，故单词需要“位置”这一属性。
/// “词性”是为了快速进行语法分析而设置的属性。
/// “文本内容”是在进行语法分析和语义分析的时候，需要进行进一步的判定而必须的内容。
/// 词法分析器的生成必须在编译器之后!!!!!!
/// </summary>
public class LexicalAnalyzer
{
    private static LexicalAnalyzer instance;
    public static LexicalAnalyzer GetInstance()
    {
        if (instance == null)
        {
            instance = new LexicalAnalyzer();
        }
        return instance;
    }
    private CharacterType readStatus;
    private string readBuffer;
    private bool floatNumberFlag;
    private int rowNum;
    private int colNum;
    private int cursor;
    private Compiler compiler;
    /// <summary>
    /// 词法分析器构造函数
    /// </summary>
    private LexicalAnalyzer()
    {
        //词法分析器的构造函数中实际上什么也不需要做，成员变量的初始化会在Analysis()方法中去完成
        ExceptionMessage.DebugLog("词法分析器生成完毕......");
    }
    /// <summary>
    /// 设置词法分析器的编译器对象
    /// </summary>
    /// <param name="compiler"></param>
    public void SetCompiler(Compiler compiler)
    {
        this.compiler = compiler;
        if(this.compiler != null)
        {
            ExceptionMessage.DebugLog("词法分析器已经成功配备编译器.....");
        }
    }
    /// <summary>
    /// 解析单词
    /// </summary>
    /// <param name="list">解析的单词内容会被添加到这个list里面去</param>
    /// <param name="text">被解析的原文本</param>
    public void Analysis(ArrayList list, string text)
    {
        ExceptionMessage.DebugLog("开始初始化词法分析变量......");
        readStatus = CharacterType.None;
        readBuffer = "";
        //这个标记位会在每次读取新单词的时候进行重置，从而当检测到两次小数点的时候会进行报错
        floatNumberFlag = false;
        //当前阅读的代码行数
        rowNum = 1;
        //当前阅读的代码列数
        colNum = 1;
        ExceptionMessage.DebugLog("准备进行词法分析......");
        for (cursor = 0; cursor < text.Length; ++cursor)
        {
            char character = text[cursor];
            //控制rowNum与colNum的变化
            ColAndRowNumIncrease(character);
            
            //尝试取得这个字符所代表的状态
            CharacterType currentStatus = GetCharacterType(character);
            
            if(currentStatus == CharacterType.None)
            {
                ExceptionMessage.DebugLog("Debug");
                ExceptionMessage.DebugLog(character.ToString());
            }
            else
            {
                ExceptionMessage.DebugLog(currentStatus.ToString());
            }

            //如果状态当前还是为空的情况
            if (readStatus == CharacterType.None)
            {
                readStatus = currentStatus;
                readBuffer = "";
                floatNumberFlag = false;
                if (currentStatus != CharacterType.None)
                {
                    readBuffer += character;
                }
            }
            //如果当前不为空，则开始比较两个状态，如果发生冲突，则要决定回滚
            else
            {
                if (readStatus == CharacterType.Name)
                {
                    //此时完全可以拓展变量名的长度
                    if (currentStatus == CharacterType.Name || currentStatus == CharacterType.Number)
                    {
                        readBuffer += character;
                    }
                    //此时无法再被识别成为变量名。无论是“符号”或者“空”
                    else
                    {
                        //自动生成单词以及游标回溯
                        CombineWordAndCursorBack(list);
                    }
                }
                else if (readStatus == CharacterType.Number)
                {
                    //如果在读取数字的过程中随后遇见了CharacterType.Name，那么此时我们会上报这个错误
                    if (currentStatus == CharacterType.Name)
                    {
                        throw new Exception(ExceptionMessage.CreateByLineAndIndex("variable's name is invalid", rowNum, colNum));
                    }
                    //数字的长度是可以扩展
                    else if (currentStatus == CharacterType.Number)
                    {
                        readBuffer += character;
                    }
                    //在检测到是符号的时候，我们需要立即对符号判定是不是小数点
                    else if (currentStatus == CharacterType.Symbol)
                    {
                        //如果确实是一个小数点，我们就需要判定是不是多次读入了小数点这种情况
                        if (character == '.')
                        {
                            if (floatNumberFlag == false)
                            {
                                readBuffer += character;
                                floatNumberFlag = true;
                            }
                            else
                            {
                                throw new Exception(ExceptionMessage.CreateByLineAndIndex("float number is invalid", rowNum, colNum));
                            }
                        }
                        //如果是其他符号，我们就可以触发合并的动作了
                        else
                        {
                            CombineWordAndCursorBack(list);
                        }
                    }
                    //当检测到是可以忽略的类型的时候，我们也会直接尝试生成
                    else
                    {
                        CombineWordAndCursorBack(list);
                    }
                }
                //在这里判定符号的时候，我们采取一种这样的方式
                //我们没有必要去把一个符号的最大长度限定为2字节，我们可以默认符号是多字节的
                //所以我们在这里依旧对符号的判定采取回溯的方式
                else
                {
                    //如果后续也是符号，我们可以尝试从双字节符号中寻找它
                    if (currentStatus == CharacterType.Symbol)
                    {
                        string SymbolBuffer = readBuffer + character;
                        if (compiler.symbolMultipleRecorder.Contains(SymbolBuffer))
                        {
                            readBuffer += character;
                            //如果双字节匹配成功，我们只需要进行合并但是不需要进行回滚
                            CombineWord(list);
                        }
                        //如果当前双字节符号匹配失败，那么我们也可以进行回滚合并
                        else
                        {
                            CombineWordAndCursorBack(list);
                        }
                    }
                    //如果后续不是符号，我们就可以直接合并了
                    else
                    {
                        CombineWordAndCursorBack(list);
                    }
                }
            }
        }
        CombineWordAndCursorBack(list);
        ExceptionMessage.DebugLog("......词法分析已经结束");
    }
    /// <summary>
    /// 根据当前传入的字符，来控制colNum与rowNum的移动
    /// </summary>
    /// <param name="character"></param>
    private void ColAndRowNumIncrease(char character)
    {
        //检测代码的行数与列数
        //如果当前是一个换行符
        if (character == '\n')
        {
            ++rowNum;
            colNum = 0;
        }
        //如果不是换行符
        else
        {
            ++colNum;
        }
    }
    /// <summary>
    /// 合并编译单词并且移动游标
    /// </summary>
    /// <param name="list"></param>
    private void CombineWordAndCursorBack(ArrayList list)
    {
        CompileWord compileWord = CreateCompileWord();
        //将生成的编译单词添加进入外部传入的序列中
        list.Add(compileWord);
        //readBuffer = ""; 
        //这里不需要对readBuffer做清空，因为每次重新准备进入接受动作的时候，会自动清空。
        //与readBuffer类似 floatNumberFlag也不需要被重置为false
        readStatus = CharacterType.None;
        //尝试让遍历指针回溯
        --cursor;
    }
    /// <summary>
    /// 合并编译单词
    /// </summary>
    /// <param name="list"></param>
    private void CombineWord(ArrayList list)
    {
        CompileWord compileWord = CreateCompileWord();
        //将生成的编译单词添加进入外部传入的序列中
        list.Add(compileWord);
        //readBuffer = ""; 
        //这里不需要对readBuffer做清空，因为每次重新准备进入接受动作的时候，会自动清空。
        //与readBuffer类似 floatNumberFlag也不需要被重置为false
        readStatus = CharacterType.None;
    }
    /// <summary>
    /// 根据一个传入原式单词字符串，来生成一个单词
    /// </summary>
    /// <returns></returns>
    private CompileWord CreateCompileWord()
    {
        CompileWord compileWord = new CompileWord();
        compileWord.colNum = this.colNum;
        compileWord.rowNum = this.rowNum;
        compileWord.content = this.readBuffer;
        return compileWord;
    }
    /// <summary>
    /// 这个方法可以传入一个“字符类型”CharacterType以及传入一个单个单词所代表字符串
    /// 根据这两个内容，就能够生成一个“编译单词”对象。
    /// </summary>
    /// <param name="readBuffer">单个单词的字串内容</param>
    /// <param name="characterType">当前读取单词的状态</param>
    /// <param name="rowNum">当前单词读取的时候的行坐标</param>
    /// <param name="colNum">当前单词读取的时候的列坐标</param>
    /// <returns></returns>
    private CharacterType GetCharacterType(char c)
    {
        if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '_')
        {
            return CharacterType.Name;
        }
        else if (c >= '0' && c <= '9')
        {
            return CharacterType.Number;
        }
        else
        {
            if (compiler.symbolSingleRecorder.Contains(c))
            {
                return CharacterType.Symbol;
            }
            else
            {
                return CharacterType.None;
            }
        }
    }
}