mergeInto(LibraryManager.library, 
{
	StoreScores: function(str)
	{
		const data = UTF8ToString(str);
		localStorage.setItem('scores', data);
    console.log("Store score", data);
  },
	LoadScores: function()
	{
		let data = localStorage.getItem('scores');
		if(data == null) data = "";
		console.log("Load scores", data);
		let size = lengthBytesUTF8(data) + 1
		let buffer = _malloc(size);
		stringToUTF8(data, buffer, size);
		return buffer;
  },
});