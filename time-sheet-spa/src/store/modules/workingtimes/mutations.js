export default {
    setWorkingTimes(state, payload) {
        state.workingTimes = payload.wts;
        state.wtCount = payload.wtcount;
    },
    setWorkingTime(state, payload) {
        state.workingTime = payload;
    }
};