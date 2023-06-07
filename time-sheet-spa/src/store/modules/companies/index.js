import mutations from "./mutations.js";
import actions from "./actions.js";
import getters from "./getters.js";

export default {
  namespaced: true,
  state() {
    return {
      companies: [
        {
          ID: 1,
          CompanyManagerID: 1,
          Name: "Vixtra Company",
          Address: "Unknown 15",
          City: "Bijeljina",
          Country: "Bosna and Heryegovina",
          Email: "aleksbn417@gmail.com",
        },
        {
          ID: 2,
          CompanyManagerID: 1,
          Name: "Coffee Company",
          Address: "Very well known 15",
          City: "Bijeljina",
          Country: "Bosna and Heryegovina",
          Email: "aleksbn417@hotmail.com",
        },
      ],
    };
  },
  mutations,
  getters,
  actions,
};
