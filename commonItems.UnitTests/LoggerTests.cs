﻿using System;
using System.IO;
using Xunit;

namespace commonItems.UnitTests {
	[Collection("Sequential")]
	[CollectionDefinition("Sequential", DisableParallelization = true)]
	public class LoggerTests {
		[Fact]
		public void ErrorMessagesLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.Error("Error message");
			Assert.Equal("[ERROR] Error message", output.ToString().TrimEnd());
		}
		[Fact]
		public void ErrorMessagesFormatLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.ErrorFormat("Error {0}", "message");
			Assert.Equal("[ERROR] Error message", output.ToString().TrimEnd());
		}

		[Fact]
		public void WarningMessagesLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.Warn("Warning message");
			Assert.Equal("[WARN] Warning message", output.ToString().TrimEnd());
		}
		[Fact]
		public void WarningMessagesFormatLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.WarnFormat("Warning {0}", "message");
			Assert.Equal("[WARN] Warning message", output.ToString().TrimEnd());
		}

		[Fact]
		public void InfoMessagesLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.Info("Info message");
			Assert.Equal("[INFO] Info message", output.ToString().TrimEnd());
		}
		[Fact]
		public void InfoMessagesFormatLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.InfoFormat("Info {0}", "message");
			Assert.Equal("[INFO] Info message", output.ToString().TrimEnd());
		}

		[Fact]
		public void NoticeMessagesLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.Notice("Notice message");
			Assert.Equal("[NOTICE] Notice message", output.ToString().TrimEnd());
		}
		[Fact]
		public void NoticeMessagesFormatLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.NoticeFormat("Notice {0}", "message");
			Assert.Equal("[NOTICE] Notice message", output.ToString().TrimEnd());
		}

		[Fact]
		public void DebugMessagesLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.Debug("Debug message");
			Assert.Equal("[DEBUG] Debug message", output.ToString().TrimEnd());
		}
		[Fact]
		public void DebugMessagesFormatLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.DebugFormat("Debug {0}", "message");
			Assert.Equal("[DEBUG] Debug message", output.ToString().TrimEnd());
		}

		[Fact]
		public void ProgressMessagesLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.Progress("Progress message");
			Assert.Equal("[PROGRESS] Progress message", output.ToString().TrimEnd());
		}
		[Fact]
		public void ProgressMessagesFormatLogged() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.ProgressFormat("Progress {0}", "message");
			Assert.Equal("[PROGRESS] Progress message", output.ToString().TrimEnd());
		}
		[Fact]
		public void LevelCanBePassedAsArgument() {
			var output = new StringWriter();
			Console.SetOut(output);
			Logger.Log(LogLevel.Debug, "1");
			Logger.Log(LogLevel.Info, "2");
			Logger.Log(LogLevel.Warn, "3");
			Logger.Log(LogLevel.Error, "4");
			Logger.Log(LogLevel.Notice, "5");
			Logger.Log(LogLevel.Progress, "6");

			var outStr = output.ToString();
			Assert.Contains("[DEBUG] 1", outStr);
			Assert.Contains("[INFO] 2", outStr);
			Assert.Contains("[WARN] 3", outStr);
			Assert.Contains("[ERROR] 4", outStr);
			Assert.Contains("[NOTICE] 5", outStr);
			Assert.Contains("[PROGRESS] 6", outStr);
		}
	}
}
