using Xunit;

namespace commonItems.UnitTests; 

[Collection("Sequential")]
[CollectionDefinition("Sequential", DisableParallelization = true)]
public class StringUtilsTests {
	[Fact]
	public void RemQuotesRemovesQuotes() {
		const string quotedString = "\"Quoted string\"";
		// ReSharper disable once InvokeAsExtensionMethod
		string unquotedString = StringUtils.RemQuotes(quotedString);
		Assert.Equal("Quoted string", unquotedString);
	}

	[Fact]
	public void RemQuotesRequiresStartingQuotes() {
		const string quotedString = "Quoted string\"";
		string unquotedString = quotedString.RemQuotes();
		Assert.Equal("Quoted string\"", unquotedString);
	}

	[Fact]
	public void RemQuotesRequiresEndingQuotes() {
		const string quotedString = "\"Quoted string";
		string unquotedString = quotedString.RemQuotes();
		Assert.Equal("\"Quoted string", unquotedString);
	}

	[Fact]
	public void RemQuotesLeavesSingleQuote() {
		const string quotedString = "\"";
		string unquotedString = quotedString.RemQuotes();
		Assert.Equal("\"", unquotedString);
	}

	[Fact]
	public void AddQuotesTurnsEmptyStringIntoQuoted() {
		const string unquotedString = "";
		// ReSharper disable once InvokeAsExtensionMethod
		string quotedString = StringUtils.AddQuotes(unquotedString);
		Assert.Equal("\"\"", quotedString);
	}

	[Fact]
	public void AddQuotesLeavesAlreadyQuotedStringAlone() {
		const string alreadyQuotedString = "\"already quoted\"";
		string quotedString = alreadyQuotedString.AddQuotes();
		Assert.Equal("\"already quoted\"", quotedString);
	}

	[Fact]
	public void AddQuotesAddsQuotesToUnquotedString() {
		const string unqQuotedString = "not quoted";
		string quotedString = unqQuotedString.AddQuotes();
		Assert.Equal("\"not quoted\"", quotedString);
	}

	[Fact]
	public void AddQuotesLeavesSingleQuote() {
		const string singleQuoteString = "\"";
		string quotedString = singleQuoteString.AddQuotes();
		Assert.Equal("\"", quotedString);
	}

	[Fact]
	public void AddQuotesLeavesImproperlyQuotedStringAsIs() {
		const string improperlyQuotedString = "\"c";
		string quotedString = improperlyQuotedString.AddQuotes();
		Assert.Equal("\"c", quotedString);
	}
}