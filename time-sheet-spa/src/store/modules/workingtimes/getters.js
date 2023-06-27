export default {
  workingTimes(state) {
    return state.workingTimes;
  },
  hasWorkingTimes(state) {
    return !!state.workingTimes && state.workingTimes.length > 0;
  },
  workingTime(state) {
    return state.workingTime;
  },
  hasWorkingTime(state) {
    return !!state.workingTime;
  },
  numberOfWorkingTimes(state) {
    return state.wtCount;
  }
};
