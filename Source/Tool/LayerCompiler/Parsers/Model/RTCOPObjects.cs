﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerCompiler.Parsers.Model
{
    /// <summary>
    /// レイヤ定義
    /// </summary>
    [Serializable]
    class LayerDefinition
    {
        #region プロパティ
        /// <summary>
        /// レイヤ名
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 中身
        /// </summary>
        public List<object> Contents { get; private set; }

        /// <summary>
        /// ベースレイヤであるかどうか
        /// </summary>
        public bool IsBase
        {
            get
            {
                return Name == "baselayer";
            }
        }

        /// <summary>
        /// レイヤ内の名前空間
        /// </summary>
        public IEnumerable<NamespaceDefinition> Namespaces
        {
            get
            {
                return Contents.FindAll((obj) => obj is NamespaceDefinition).Cast<NamespaceDefinition>();
            }
        }

        /// <summary>
        /// レイヤ内のレイヤードなクラス定義
        /// </summary>
        public IEnumerable<LayerdClassDefinition> LayerdClassDefinitions
        {
            get
            {
                return Contents.FindAll((obj) => obj is LayerdClassDefinition).Cast<LayerdClassDefinition>();
            }
        }

        /// <summary>
        /// メソッド実装
        /// </summary>
        public IEnumerable<MethodImplementation> MethodImplementations
        {
            get
            {
                return Contents.FindAll((obj) => obj is MethodImplementation).Cast<MethodImplementation>();
            }
        }

        public IEnumerable<EventHandlerDefinition> EventHandlerDefinitions
        {
            get
            {
                return Contents.FindAll((obj) => obj is EventHandlerDefinition).Cast<EventHandlerDefinition>();
            }
        }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// レイヤ定義
        /// </summary>
        /// <param name="name">レイヤ名</param>
        /// <param name="objects">レイヤ定義の中身</param>
        public LayerDefinition(string name, IEnumerable<object> objects)
        {
            Name = name;
            Contents = new List<object>(objects);
        }

        #endregion

        #region メソッド
        /// <summary>
        /// 文字列を返す
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            string result = "";
            // キーワードと名前
            if (IsBase)
            {
                result += "baselayer";
            }
            else
            {
                result += "layer " + Name;
            }
            // 中身
            result += "\r\n{\r\n";
            foreach (var content in Contents)
            {
                if (content is IgnoreObject)
                {
                    if (((IgnoreObject)content).Content is PreprocessDirective)
                    {
                        result += (content + "\r\n");
                    }
                    else if (!(content is IgnoreObjectBlock))
                    {
                        string text = content.ToString();
                        if ((text == ";") || (text == ":"))
                        {
                            result += (text + "\r\n");
                        }
                        else
                        {
                            result += (text + " ");
                        }
                    }
                    else
                    {
                        result += ("\r\n" + content + "\r\n");
                    }
                }
                else
                {
                    result += (content + "\r\n");
                }
            }
            result += "\r\n}";

            return result;
        }

        #endregion
    }

    /// <summary>
    /// 名前空間の定義
    /// </summary>
    [Serializable]
    class NamespaceDefinition
    {
        #region プロパティ
        /// <summary>
        /// 名前空間の名前
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// インライン名前空間であるかどうか
        /// </summary>
        public bool IsInline { get; protected set; }

        /// <summary>
        /// 中身
        /// </summary>
        public List<object> Contents { get; private set; }

        /// <summary>
        /// ネストされている名前空間
        /// </summary>
        public IEnumerable<NamespaceDefinition> Namespaces
        {
            get
            {
                return Contents.FindAll((obj) => obj is NamespaceDefinition).Cast<NamespaceDefinition>();
            }
        }

        /// <summary>
        /// レイヤードなクラス定義
        /// </summary>
        public IEnumerable<LayerdClassDefinition> LayerdClassDefinitions
        {
            get
            {
                return Contents.FindAll((obj) => obj is LayerdClassDefinition).Cast<LayerdClassDefinition>();
            }
        }

        /// <summary>
        /// メソッド実装
        /// </summary>
        public IEnumerable<MethodImplementation> MethodImplementations
        {
            get
            {
                return Contents.FindAll((obj) => obj is MethodImplementation).Cast<MethodImplementation>();
            }
        }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// レイヤ定義
        /// </summary>
        /// <param name="name">レイヤ名</param>
        /// <param name="objects">レイヤ定義の中身</param>
        /// <param name="isInline">インタライン名前空間であるかどうか</param>
        public NamespaceDefinition(string name, IEnumerable<object> objects, bool isInline)
        {
            Name = name;
            Contents = new List<object>(objects);
            IsInline = isInline;
        }

        #endregion

        #region メソッド
        /// <summary>
        /// 文字列を返す
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            string result = "";
            // インライン
            if (IsInline)
                result += "inline ";
            // キーワード
            result += "namespace ";
            // 名前
            result += Name;
            // 中身
            result += "\r\n{\r\n";
            foreach (var content in Contents)
            {
                if (content is IgnoreObject)
                {
                    if (((IgnoreObject)content).Content is PreprocessDirective)
                    {
                        result += (content + "\r\n");
                    }
                    else if (!(content is IgnoreObjectBlock))
                    {
                        string text = content.ToString();
                        if ((text == ";") || (text == ":"))
                        {
                            result += (text + "\r\n");
                        }
                        else
                        {
                            result += (text + " ");
                        }
                    }
                    else
                    {
                        result += ("\r\n" + content + "\r\n");
                    }
                }
                else
                {
                    result += (content + "\r\n");
                }
            }
            result += "\r\n}";

            return result;
        }

        #endregion

    }

    /// <summary>
    /// レイヤードクラスの定義
    /// </summary>
    [Serializable]
    class LayerdClassDefinition
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// class か struct
        /// </summary>
        public string ClassKey { get; protected set; }

        /// <summary>
        /// ベースクラスであるかどうか
        /// </summary>
        public bool? IsBase { get; protected set; }

        /// <summary>
        /// スーパークラス
        /// </summary>
        public List<SuperClassDefinition> SuperClasses { get; private set; }

        /// <summary>
        /// 中身
        /// </summary>
        public List<object> Contents { get; private set; }

        /// <summary>
        /// レイヤードなクラス定義
        /// </summary>
        public IEnumerable<LayerdClassDefinition> LayerdClassDefinitions
        {
            get
            {
                return Contents.FindAll((obj) => obj is LayerdClassDefinition).Cast<LayerdClassDefinition>();
            }
        }

        /// <summary>
        /// コンストラクタの定義
        /// </summary>
        public IEnumerable<ConstructorDefinition> ConstructorDefinitions
        {
            get
            {
                return Contents.FindAll((obj) => obj is ConstructorDefinition).Cast<ConstructorDefinition>();
            }
        }

        /// <summary>
        /// デストラクタの定義
        /// </summary>
        public DestructorDefinition DestructorDefinitions
        {
            get
            {
                return (DestructorDefinition)Contents.Find((obj) => obj is DestructorDefinition);
            }
        }

        /// <summary>
        /// メソッドの定義
        /// </summary>
        public IEnumerable<LayerdMethodDefinition> MethodDefinitions
        {
            get
            {
                return Contents.FindAll((obj) => obj is LayerdMethodDefinition).Cast<LayerdMethodDefinition>();
            }
        }

        /// <summary>
        /// イベントハンドラの定義
        /// </summary>
        public IEnumerable<EventHandlerDefinition> EventHandlerDefinitions
        {
            get
            {
                return Contents.FindAll((obj) => obj is EventHandlerDefinition).Cast<EventHandlerDefinition>();
            }
        }

        /// <summary>
        /// メンバ変数の宣言
        /// </summary>
        public IEnumerable<VariableDeclaration> LayerMemberDeclaration
        {
            get
            {
                return Contents.FindAll((obj) => obj is VariableDeclaration).Cast<VariableDeclaration>();
            }
        }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// レイヤードクラスの定義
        /// </summary>
        /// <param name="name">クラス名</param>
        /// <param name="key">class か struct</param>
        /// <param name="supers">スーパークラス</param>
        /// <param name="objects">レイヤ定義の中身</param>
        /// <param name="isBase">ベースクラスであるかどうか</param>
        public LayerdClassDefinition(string name, string key, IEnumerable<SuperClassDefinition> supers, IEnumerable<object> objects, bool? isBase = null)
        {
            Name = name;
            ClassKey = key;
            if (supers != null)
            {
                SuperClasses = new List<SuperClassDefinition>(supers);
            }
            Contents = new List<object>(objects);
            IsBase = isBase;
        }

        #endregion

        #region メソッド
        /// <summary>
        /// IsBaseのセット
        /// </summary>
        /// <param name="isBase">IsBase</param>
        /// <returns>thisポインタ</returns>
        public LayerdClassDefinition SetIsBase(bool? isBase)
        {
            IsBase = isBase;
            return this;
        }

        /// <summary>
        /// 文字列を返す
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            string result = "";
            // キーワード
            if (IsBase != null)
            {
                if (IsBase.Value) result = "base ";
                else result = "partial ";
            }
            // class key
            result += (ClassKey + " ");
            // 名前
            result += Name;
            // スーパークラス
            if (SuperClasses.Count > 0)
            {
                result += " : ";
                result += string.Join(", ", SuperClasses);
            }
            // 中身
            result += "\r\n{\r\n";
            foreach (var content in Contents)
            {
                if (content is IgnoreObject)
                {
                    if (((IgnoreObject)content).Content is PreprocessDirective)
                    {
                        result += (content + "\r\n");
                    }
                    else if (!(content is IgnoreObjectBlock))
                    {
                        string text = content.ToString();
                        if ((text == ";") || (text == ":"))
                        {
                            result += (text + "\r\n");
                        }
                        else
                        {
                            result += (text + " ");
                        }
                    }
                    else
                    {
                        result += ("\r\n" + content + "\r\n");
                    }
                }
                else
                {
                    result += (content + "\r\n");
                }
            }
            result += "\r\n};";

            return result;
        }

        #endregion

    }

    /// <summary>
    /// アクセス修飾子
    /// </summary>
    [Serializable]
    class AccessModifier
    {
        #region プロパティ
        /// <summary>
        /// 種類
        /// </summary>
        public AccessKind Kind { get; protected set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// アクセス修飾子
        /// </summary>
        /// <param name="text">テキスト</param>
        public AccessModifier(string text)
        {
            if (text == "public")
            {
                Kind = AccessKind.Public;
            }
            else if (text == "protected")
            {
                Kind = AccessKind.Protected;
            }
            else if (text == "private")
            {
                Kind = AccessKind.Private;
            }
        }

        #endregion

        #region メソッド
        /// <summary>
        /// 文字列を返す
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            string result = "";
            switch (Kind)
            {
                case AccessKind.Public:
                    result = "public:";
                    break;
                case AccessKind.Protected:
                    result = "protected:";
                    break;
                case AccessKind.Private:
                    result = "private:";
                    break;
            }
            return result;
        }

        #endregion

        #region 型
        /// <summary>
        /// アクセス修飾子の種類
        /// </summary>
        [Serializable]
        public enum AccessKind
        {
            None,
            Public,
            Protected,
            Private,
        }

        #endregion

    }

    /// <summary>
    /// メソッド実装
    /// </summary>
    [Serializable]
    class MethodImplementation
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public string FullName { get; protected set; }

        /// <summary>
        /// 名前のメソッド名の部分
        /// </summary>
        public string MethodName
        {
            get
            {
                string[] s = FullName.Split(new string[] { "::" }, StringSplitOptions.None);
                string result = s[s.Length - 1];
                return result;
            }
        }

        /// <summary>
        /// 名前のクラス名の部分
        /// </summary>
        public string ClassName
        {
            get
            {
                string[] s = FullName.Split(new string[] { "::" }, StringSplitOptions.None);
                string result = string.Join("::", s, 0, (s.Length - 1));
                return result;
            }
        }

        /// <summary>
        /// 戻り値の型
        /// </summary>
        public VariableType ReturnType { get; protected set; }

        /// <summary>
        /// パラメータ
        /// </summary>
        public List<VariableDeclaration> Parameters { get; private set; }

        /// <summary>
        /// コンテンツ
        /// </summary>
        public IgnoreObjectBlock Contents { get; protected set; }

        /// <summary>
        /// thisポインタの修飾子
        /// </summary>
        public List<string> ThisModifiers { get; private set; }

        /// <summary>
        /// noexceptであるかどうか
        /// </summary>
        public bool IsNoexcept { get; protected set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// メソッド実装
        /// </summary>
        /// <param name="name">メソッド名</param>
        /// <param name="returnType">戻り値の型</param>
        /// <param name="parameters">パラメータ</param>
        /// <param name="contents">コンテンツ</param>
        /// <param name="thisModifiers">修飾子</param>
        /// <param name="isNoexcept">noexceptであるかどうか</param>
        public MethodImplementation(string name, VariableType returnType, IEnumerable<VariableDeclaration> parameters, IgnoreObjectBlock contents, IEnumerable<string> thisModifiers, bool isNoexcept)
        {
            FullName = name;
            ReturnType = returnType;
            Parameters = new List<VariableDeclaration>(parameters);
            Contents = contents;
            ThisModifiers = new List<string>(thisModifiers);
            IsNoexcept = isNoexcept;
        }

        #endregion

        #region メソッド
        /// <summary>
        /// メソッド実装に対応するメソッド定義を返す
        /// </summary>
        /// <returns>メソッド定義</returns>
        public LayerdMethodDefinition ToLayerdMethodDefinition()
        {
            var result = new LayerdMethodDefinition(MethodName, ReturnType, Parameters, Contents, new string[] { }, ThisModifiers, IsNoexcept, false);
            return result;
        }

        /// <summary>
        /// 文字列を返す
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            string result = "";
            // 戻り値型
            result += (ReturnType + " ");
            // メソッド名
            result += (FullName + " (");
            // パラメータ
            result += string.Join(", ", Parameters);
            result += ")";
            // 修飾子
            foreach (string modifier in ThisModifiers)
            {
                result += (" " + modifier);
            }
            // noexcept
            if (IsNoexcept) result += " noexcept";
            // コンテンツ
            result += "\r\n";
            result += Contents.ToString();

            return result;
        }

        #endregion
    }

    /// <summary>
    /// コンストラクタの定義
    /// </summary>
    [Serializable]
    class ConstructorDefinition
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// パラメータ
        /// </summary>
        public List<VariableDeclaration> Parameters { get; private set; }

        /// <summary>
        /// コンテンツ
        /// </summary>
        public object Contents { get; internal set; }

        /// <summary>
        /// 修飾子
        /// </summary>
        public List<string> Modifiers { get; private set; }

        /// <summary>
        /// noexceptであるかどうか
        /// </summary>
        public bool IsNoexcept { get; protected set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// レイヤードメソッドの定義
        /// </summary>
        /// <param name="name">メソッド名</param>
        /// <param name="parameters">パラメータ</param>
        /// <param name="contents">コンテンツ</param>
        /// <param name="modifiers">修飾子</param>
        /// <param name="isNoexcept">noexceptであるかどうか</param>
        public ConstructorDefinition(string name, IEnumerable<VariableDeclaration> parameters, object contents, IEnumerable<string> modifiers, bool isNoexcept)
        {
            Name = name;
            Parameters = new List<VariableDeclaration>(parameters);
            Contents = contents;
            Modifiers = new List<string>(modifiers);
            IsNoexcept = isNoexcept;
        }

        #endregion

        #region メソッド
        /// <summary>
        /// 文字列を返す
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            string result = "";
            // 修飾子
            foreach (string modifier in Modifiers)
            {
                result += (modifier + " ");
            }
            // メソッド名
            result += (Name + " (");
            // パラメータ
            result += string.Join(", ", Parameters);
            result += ")";
            // noexcept
            if (IsNoexcept) result += " noexcept";
            // コンテンツ
            if (Contents is IgnoreObjectBlock)
                result += "\r\n";
            result += Contents.ToString();

            return result;
        }

        #endregion

    }

    /// <summary>
    /// デストラクタの定義
    /// </summary>
    [Serializable]
    class DestructorDefinition
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// コンテンツ
        /// </summary>
        public object Contents { get; protected set; }

        /// <summary>
        /// 修飾子
        /// </summary>
        public List<string> Modifiers { get; private set; }

        /// <summary>
        /// noexceptであるかどうか
        /// </summary>
        public bool IsNoexcept { get; protected set; }

        /// <summary>
        /// overrideであるかどうか
        /// </summary>
        public bool IsOverride { get; protected set; }

        /// <summary>
        /// 仮想関数であるかどうか
        /// </summary>
        public bool IsVirtual
        {
            get { return Modifiers.Contains("virtual"); }
        }

        /// <summary>
        /// 純粋仮想関数であるか
        /// </summary>
        public bool IsPureVirtual
        {
            get
            {
                return (Contents.ToString() == " = 0 ;");
            }
        }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// デストラクタの定義
        /// </summary>
        /// <param name="name">メソッド名</param>
        /// <param name="contents">コンテンツ</param>
        /// <param name="modifiers">修飾子</param>
        /// <param name="isNoexcept">noexceptであるかどうか</param>
        /// <param name="isOverride">overrideであるかどうか</param>
        public DestructorDefinition(string name, object contents, IEnumerable<string> modifiers, bool isNoexcept, bool isOverride)
        {
            Name = name;
            Contents = contents;
            Modifiers = new List<string>(modifiers);
            IsNoexcept = isNoexcept;
            IsOverride = isOverride;
        }

        #endregion

        #region メソッド
        /// <summary>
        /// 文字列を返す
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            string result = "";
            // 修飾子
            foreach (string modifier in Modifiers)
            {
                result += (modifier + " ");
            }
            // メソッド名
            result += ("~" + Name + " ()");
            // noexcept
            if (IsNoexcept) result += " noexcept";
            // override
            if (IsOverride) result += " override";
            // コンテンツ
            if (Contents is IgnoreObjectBlock)
                result += "\r\n";
            result += Contents.ToString();

            return result;
        }

        #endregion

    }

    /// <summary>
    /// レイヤードメソッドの定義
    /// </summary>
    [Serializable]
    class LayerdMethodDefinition
    {
        #region プロパティ
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 戻り値の型
        /// </summary>
        public VariableType ReturnType { get; protected set; }

        /// <summary>
        /// パラメータ
        /// </summary>
        public List<VariableDeclaration> Parameters { get; private set; }

        /// <summary>
        /// コンテンツ
        /// </summary>
        public object Contents { get; protected set; }

        /// <summary>
        /// 修飾子
        /// </summary>
        public List<string> Modifiers { get; private set; }

        /// <summary>
        /// thisポインタの修飾子
        /// </summary>
        public List<string> ThisModifiers { get; private set; }

        /// <summary>
        /// noexceptであるかどうか
        /// </summary>
        public bool IsNoexcept { get; protected set; }

        /// <summary>
        /// overrideであるかどうか
        /// </summary>
        public bool IsOverride { get; protected set; }

        /// <summary>
        /// 仮想関数であるかどうか
        /// </summary>
        public bool IsVirtual
        {
            get { return Modifiers.Contains("virtual"); }
        }

        /// <summary>
        /// 純粋仮想関数であるか
        /// </summary>
        public bool IsPureVirtual
        {
            get
            {
                return (Contents.ToString() == " = 0 ;");
            }
        }

        /// <summary>
        /// ベースメソッドであるかどうか
        /// </summary>
        public bool? IsBase { get; protected set; }

        /// <summary>
        /// constであるかどうか
        /// </summary>
        public bool IsConst
        {
            get
            {
                return ThisModifiers.Contains("const");
            }
        }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// レイヤードメソッドの定義
        /// </summary>
        /// <param name="name">メソッド名</param>
        /// <param name="returnType">戻り値の型</param>
        /// <param name="parameters">パラメータ</param>
        /// <param name="contents">コンテンツ</param>
        /// <param name="modifiers">修飾子</param>
        /// <param name="thisModifiers">修飾子</param>
        /// <param name="isNoexcept">noexceptであるかどうか</param>
        /// <param name="isOverride">overrideであるかどうか</param>
        /// <param name="isBase">ベースメソッドであるかどうか</param>
        public LayerdMethodDefinition(string name, VariableType returnType, IEnumerable<VariableDeclaration> parameters, object contents, IEnumerable<string> modifiers, IEnumerable<string> thisModifiers, bool isNoexcept, bool isOverride, bool? isBase = null)
        {
            Name = name;
            ReturnType = returnType;
            Parameters = new List<VariableDeclaration>(parameters);
            Contents = contents;
            Modifiers = new List<string>(modifiers);
            ThisModifiers = new List<string>(thisModifiers);
            IsNoexcept = isNoexcept;
            IsOverride = isOverride;
            IsBase = isBase;
        }

        #endregion

        #region メソッド
        /// <summary>
        /// IsBaseのセット
        /// </summary>
        /// <param name="isBase">IsBase</param>
        /// <returns>thisポインタ</returns>
        public LayerdMethodDefinition SetIsBase(bool? isBase)
        {
            IsBase = isBase;
            return this;
        }

        /// <summary>
        /// メソッド定義が内容的に同じであるか
        /// </summary>
        /// <param name="obj">比較対象</param>
        /// <returns>内容的に同じであるかどうか</returns>
        public bool CompareMethod(LayerdMethodDefinition obj)
        {
            if ((obj.Name == Name) && (obj.Parameters.Count == Parameters.Count))
            {
                int n = obj.Parameters.Count;
                for (int i = 0; i < n; ++i)
                {
                    if (!obj.Parameters[i].Type.CompareType(Parameters[i].Type))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 文字列を返す
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            string result = "";
            // キーワード
            if (IsBase != null)
            {
                if (IsBase.Value) result = "base ";
                else result = "partial ";
            }
            // 修飾子
            foreach (string modifier in Modifiers)
            {
                result += (modifier + " ");
            }
            // 戻り値型
            result += (ReturnType + " ");
            // メソッド名
            result += (Name + " (");
            // パラメータ
            result += string.Join(", ", Parameters);
            result += ")";
            // 修飾子
            foreach (string modifier in ThisModifiers)
            {
                result += (" " + modifier);
            }
            // noexcept
            if (IsNoexcept) result += " noexcept";
            // override
            if (IsOverride) result += " override";
            // コンテンツ
            if (Contents is IgnoreObjectBlock)
                result += "\r\n";
            result += Contents.ToString();

            return result;
        }

        #endregion

    }

    /// <summary>
    /// イベントハンドラの定義
    /// </summary>
    [Serializable]
    class EventHandlerDefinition : LayerdMethodDefinition
    {
        #region プロパティ
        /// <summary>
        /// イベント名
        /// </summary>
        public string EventName { get; protected set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// イベントハンドラの定義
        /// </summary>
        /// <param name="eventName">イベント名</param>
        /// <param name="method">メソッド情報</param>
        public EventHandlerDefinition(string eventName, LayerdMethodDefinition method)
            : base(method.Name, method.ReturnType, method.Parameters, method.Contents, method.Modifiers, method.ThisModifiers, method.IsNoexcept, method.IsOverride)
        {
            EventName = eventName;
        }

        #endregion

        #region メソッド
        /// <summary>
        /// 文字列を返す
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString()
        {
            // イベントハンドラの記述
            string result = "[eventhandler(";
            result += EventName;
            result += ")]\r\n";
            // メソッドの定義
            result += base.ToString();
            // 結果を返す
            return result;
        }

        #endregion

    }

}
