using System;
using System.Collections.Generic;
using System.Text;

namespace TestTool.DAL
{
	public class Reply
	{
		public string Text { get; set; }
		public bool IsCorrect { get; set; }

		//public bool Selected { get; set; }
		
	}
	//public class ReplySelection
	//{
	//	public string Text { get; set; }
	//	public bool IsCorrect { get; set; }
		
	//	public bool Selected {
	//		get; set;
	//	}

	//	public ReplySelection()
	//	{
	//		this.Selected = false;
	//	}
	//	public ReplySelection(Reply reply)
	//	{
	//		this.Text = reply.Text;
	//		this.IsCorrect = reply.IsCorrect;
	//	}
	//}
}
