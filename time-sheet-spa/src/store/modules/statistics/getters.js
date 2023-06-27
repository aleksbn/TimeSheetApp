export default {
  hasStatistics(state) {
    return !!state.statistics && state.statistics.length > 0;
  },
  statistics(state) {
    return state.statistics;
  },
};
