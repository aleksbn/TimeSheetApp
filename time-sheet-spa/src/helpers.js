import { API_URL } from "./config";
import { TIMEOUT_SECONDS } from "./config";

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

export const AjaxCall = async function (
	link,
	method,
	bodyData,
	authData,
	longOperation = false
) {
	const requestSettings = {
		method,
		headers: {
			Accept: "application/json",
			"Content-Type": "application/json",
			...authData,
			body: JSON.stringify(bodyData),
		},
	};
	if (bodyData !== null) {
		requestSettings.body = JSON.stringify(bodyData);
	}

	const request = fetch(`${API_URL}${link}`, requestSettings);
	const res = await Promise.race([
		request,
		timeout(longOperation === false ? TIMEOUT_SECONDS : 120),
	]);
	const data = await res.json();

	if (!res.ok) {
		const error = new Error(data || "Failed to create new user!");
		throw error;
	}

	return data;
};
