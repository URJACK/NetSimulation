using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 编译、执行器是核心模块，
/// 它可以解析文本，并且根据文本生成指令
/// </summary>
public class Compiler
{
    private static Compiler instance;
    /// <summary>
    /// 取得编译器实例
    /// </summary>
    /// <returns></returns>
    public static Compiler GetInstance()
    {
        if (instance == null)
        {
            instance = new Compiler();
        }
        return instance;
    }
    /// <summary>
    /// 关键字记录器
    /// 用来存储编译过程中所有的关键字
    /// </summary>
    public HashSet<string> keywordRecorder;
    /// <summary>
    /// 单符号记录器
    /// 编译器能够识别的所有的单字节符号的集合
    /// </summary>
    public HashSet<char> symbolSingleRecorder;
    /// <summary>
    /// 多符号记录器
    /// 编译器能够识别的所有多字节符号的集合
    /// </summary>
    public HashSet<string> symbolMultipleRecorder;
    /// <summary>
    /// 编译单词记录器
    /// 词法分析器将分析之后得到的所有单词全部放入记录器之中
    /// </summary>
    public ArrayList compileWords;
    /// <summary>
    /// 词法分析器
    /// 能够分析原式字串的内容，并将他们转为单词赋给编译器。
    /// </summary>
    private LexicalAnalyzer lexicalAnalyzer;
    /// <summary>
    /// 构造函数
    /// 我们需要优先尝试生成关键字记录器
    /// </summary>
    private Compiler()
    {
        CompilerInitial compilerInitial = CompilerInitial.GetInstance();
        this.keywordRecorder = new HashSet<string>();
        compilerInitial.InitKeywordRecorder(this.keywordRecorder);
        this.compileWords = new ArrayList();
        this.symbolSingleRecorder = new HashSet<char>();
        this.symbolMultipleRecorder = new HashSet<string>();
        compilerInitial.InitSymbolRecorder(this.symbolSingleRecorder, this.symbolMultipleRecorder);
        this.lexicalAnalyzer = LexicalAnalyzer.GetInstance();
        this.lexicalAnalyzer.SetCompiler(this);
        ExceptionMessage.DebugLog("编译器生成完毕......");
    }
    /// <summary>
    /// 读取初始文本应该调用的函数
    /// 在读取完初始文本之后，我们会尝试着对初始文本进行词法分析。
    /// </summary>
    public void CompileWithSourceCode(string str)
    {
        //先将已经编译获得的单词集合进行一次清空
        if (this.compileWords != null)
        {
            this.compileWords.Clear();
        }
        else
        {
            ExceptionMessage.DebugLog("编译单词序列出现空指针的错误");
        }
        try
        {
            //开始词法分析
            this.lexicalAnalyzer.Analysis(this.compileWords, str);
        }
        catch (Exception e)
        {
            //抓出异常并打印
            ExceptionMessage.DebugLog(e.Message);
        }
    }
    
}