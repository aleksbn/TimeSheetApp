export const ResetLocalStorage = function () {
	localStorage.removeItem("comid");
	localStorage.removeItem("depid");
	localStorage.removeItem("empid");
	localStorage.removeItem("comidwt");
	localStorage.removeItem("depidwt");
	localStorage.removeItem("empidwt");
};

export const timeout = function (s) {
	return new Promise(function (_, reject) {
		setTimeout(function () {
			reject(
				new Error(`Server request took too long! Timeout after ${s} second`)
			);
		}, s * 1000);
	});
};
