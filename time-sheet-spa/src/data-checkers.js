import moment from "moment";
export default {
	checkPersonName(value) {
		const NAME_REGEX = /^[a-zA-Z]+([ \-']{0,1}[a-zA-Z]+){0,2}[.]{0,1}$/;
		return NAME_REGEX.test(value);
	},
	checkEmail(value) {
		const EMAIL_REGEX =
			/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
		return EMAIL_REGEX.test(value);
	},
	checkPassword(value) {
		const PASSWORD_REGEX =
			/^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,50}$/;
		return PASSWORD_REGEX.test(value);
	},
	checkCity(value) {
		const CITY_REGEX =
			/^(?:[A-Za-z]{2,}(?:(\.\s|'s\s|\s?-\s?|\s)?(?=[A-Za-z]+))){1,2}(?:[A-Za-z]+)?$/;
		return CITY_REGEX.test(value);
	},
	checkCountry(value) {
		const COUNTRY_REGEX =
			/^(?:[A-Za-z]{2,}(?:(\.\s|'s\s|\s?-\s?|\s)?(?=[A-Za-z]+))){1,2}(?:[A-Za-z]+)?$/;
		return COUNTRY_REGEX.test(value);
	},
	checkDate(value) {
		return moment(value, "DD.MM.YYYY", true).isValid();
	},
	checkPhone(value) {
		const PHONE_REGEX = /^[+]?[0-9]{3,25}$/;
		return PHONE_REGEX.test(value);
	},
	checkDegree(value) {
		const DEGREE_REGEX = /^[A-Za-z\s]*$/;
		return DEGREE_REGEX.test(value);
	},
};
