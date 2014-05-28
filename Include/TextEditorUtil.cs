using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace System
{
	internal static class TextEditorUtil
	{
		
		private static readonly Font DefaultTextEditorFont = new Font("Ubuntu Mono", 10f, GraphicsUnit.Point);
		
		public static List<string> GetHighlighters()
		{
			List<string> list = new List<string>();
			foreach (DictionaryEntry dictionaryEntry in HighlightingManager.Manager.HighlightingDefinitions)
			{
				list.Add(string.Format("{0}", dictionaryEntry.Key));
			}
			return list;
		}
		
		public static void SetText(this TextEditorControl textEditorControl1, string text)
		{
			textEditorControl1.ResetText();
			textEditorControl1.Text = text;
			textEditorControl1.Document.RequestUpdate(new TextAreaUpdate(0));
			textEditorControl1.Font = TextEditorUtil.DefaultTextEditorFont;
			textEditorControl1.ActiveTextAreaControl.Font = TextEditorUtil.DefaultTextEditorFont;
		}
	}
}
