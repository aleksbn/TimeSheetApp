export default {
    setWorkingTimes(state, payload) {
        state.workingTimes = payload.wts;
        state.wtCount = payload.wtcount;
    },
};