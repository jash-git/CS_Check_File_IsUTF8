C# 判斷 檔案是否為UTF8格式 [任何程式語言都可參考此作法 ~ 此程式包含判斷ANSI、UTF8、Unicode的技巧，但是我單純改寫成判斷UTF8]


資料來源:https://blog.xuite.net/jaofeng.chen/DesignShow/4702957-%E7%94%A8%E7%A8%8B%E5%BC%8F%E5%88%A4%E6%96%B7%E6%96%87%E5%AD%97%E6%AA%94%E7%9A%84%E6%AA%94%E6%A1%88%E7%B7%A8%E7%A2%BCEncoding
https://docs.microsoft.com/zh-tw/dotnet/api/system.text.encoding.getencoding?view=netframework-4.8


GITHUB: https://github.com/jash-git/CS_Check_File_IsUTF8


此程式包含判斷ANSI、UTF8、Unicode的技巧，但是我單純改寫成判斷UTF8]

static bool IsUTF8File (String StrName)
{
	bool blnAns = false;

	Stream reader = File.Open(StrName, FileMode.Open, FileAccess.Read);
	Encoding encoder = null;
	byte[] header = new byte[4];
	// 讀取前四個Byte
	reader.Read(header, 0, 4);
	if (header[0] == 0xFF && header[1] == 0xFE)
	{
		// UniCode File
		reader.Position = 2;
		encoder = Encoding.Unicode;
	}
	else if (header[0] == 0xEF && header[1] == 0xBB && header[2] == 0xBF)
	{
		// UTF-8 File
		reader.Position = 3;
		encoder = Encoding.UTF8;
		blnAns = true;
	}
	else
	{
		// Default Encoding File
		reader.Position = 0;
		encoder = Encoding.Default;
	}

	reader.Close();
	// .......... 接下來的程式

	return blnAns;
}