import mutations from "./mutations.js";
import actions from "./actions.js";
import getters from "./getters.js";

export default {
	namespaced: true,
	state() {
		return {
			employees: [],
			emCount: 0,
			employee: null,
		};
	},
	mutations,
	getters,
	actions,
};
