// Type: System.Web.HttpServerUtilityBase
// Assembly: System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// Assembly location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Web.dll

using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace System.Web
{
    [TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
    public abstract class HttpServerUtilityBase
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected HttpServerUtilityBase();

        public virtual string MachineName { get; }
        public virtual int ScriptTimeout { get; set; }

        public virtual void ClearError();
        public virtual object CreateObject(string progID);
        public virtual object CreateObject(Type type);
        public virtual object CreateObjectFromClsid(string clsid);
        public virtual void Execute(string path);
        public virtual void Execute(string path, TextWriter writer);
        public virtual void Execute(string path, bool preserveForm);
        public virtual void Execute(string path, TextWriter writer, bool preserveForm);
        public virtual void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm);
        public virtual Exception GetLastError();
        public virtual string HtmlDecode(string s);
        public virtual void HtmlDecode(string s, TextWriter output);
        public virtual string HtmlEncode(string s);
        public virtual void HtmlEncode(string s, TextWriter output);
        public virtual string MapPath(string path);
        public virtual void Transfer(string path, bool preserveForm);
        public virtual void Transfer(string path);
        public virtual void Transfer(IHttpHandler handler, bool preserveForm);
        public virtual void TransferRequest(string path);
        public virtual void TransferRequest(string path, bool preserveForm);
        public virtual void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers);
        public virtual string UrlDecode(string s);
        public virtual void UrlDecode(string s, TextWriter output);
        public virtual string UrlEncode(string s);
        public virtual void UrlEncode(string s, TextWriter output);
        public virtual string UrlPathEncode(string s);
        public virtual byte[] UrlTokenDecode(string input);
        public virtual string UrlTokenEncode(byte[] input);
    }
}
