import { Constants } from "../Constants";
import objectPath from "object-path";

export function CreditCardStatusReducer(state = {
  creditCardStatus: {
    fetchAll: {
      isFetching: false,
      didInvalidate: false,
      results: []
    },
    pagination: {
      pageNumber: 1,
      pageSize: 10
    }
  }
}, action) {
  try {
    objectPath.ensureExists(state, "creditCardStatus.pagination", { pageNumber: 1, pageSize: 10 })
    switch (action.type) {
      case Constants.creditCardStatus.SET_GRID_PAGINATION:
        return paginationPagination(state, action);
      case Constants.creditCardStatus.RELOAD:
        return Object.assign({}, state, {
          creditCardStatus: {
            ...state.creditCardStatus,
            reload: true
          }
        });
      case Constants.creditCardStatus.FETCH_ALL_REQUEST:
      case Constants.creditCardStatus.FETCH_ALL_SUCCESS:
      case Constants.creditCardStatus.FETCH_ALL_FAILURE:
        return fetchAll(state, action);
      case Constants.creditCardStatus.FETCH_SINGLE_REQUEST:
      case Constants.creditCardStatus.FETCH_SINGLE_SUCCESS:
      case Constants.creditCardStatus.FETCH_SINGLE_FAILURE:
        return fetchSingle(state, action);
      case Constants.creditCardStatus.POST_SINGLE_REQUEST:
      case Constants.creditCardStatus.POST_SINGLE_SUCCESS:
      case Constants.creditCardStatus.POST_SINGLE_FAILURE:
        return postSingle(state, action);
      case Constants.creditCardStatus.DELETE_SINGLE_REQUEST:
      case Constants.creditCardStatus.DELETE_SINGLE_SUCCESS:
      case Constants.creditCardStatus.DELETE_SINGLE_FAILURE:
        return deleteSingle(state, action);
      case Constants.creditCardStatus.UPDATE_SINGLE_REQUEST:
      case Constants.creditCardStatus.UPDATE_SINGLE_SUCCESS:
      case Constants.creditCardStatus.UPDATE_SINGLE_FAILURE:
        return updateSingle(state, action);
      default:
        return state;
    }
  } catch (err) {
    return state;
  }
}

function fetchAll(state, action) {
  switch (action.type) {
    case Constants.creditCardStatus.FETCH_ALL_REQUEST:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          fetchAll: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCardStatus.FETCH_ALL_SUCCESS:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          fetchAll: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCardStatus.FETCH_ALL_FAILURE:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          fetchAll: {
            isFetching: false,
            didInvalidate: false,
            error: action.data
          }
        }
      });
    default:
      return state;
  }
}

function paginationPagination(state, action) {
  const page = action.data && action.data.pageNumber ? action.data.pageNumber : state.creditCardStatus.pagination.pageNumber;
  const pageSize = action.data && action.data.pageSize ? action.data.pageSize : state.creditCardStatus.pagination.pageSize;
  return Object.assign({}, state, {
    creditCardStatus: {
      ...state.creditCardStatus,
      pagination: {
        pageNumber: page,
        pageSize: pageSize
      }
    }
  });
}

function fetchSingle(state, action) {
  switch (action.type) {
    case Constants.creditCardStatus.FETCH_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          fetchSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCardStatus.FETCH_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          fetchSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCardStatus.FETCH_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          fetchSingle: {
            isFetching: false,
            didInvalidate: false,
            error: action.data
          }
        }
      });
    default:
      return state;
  }
}

function postSingle(state, action) {
  switch (action.type) {
    case Constants.creditCardStatus.POST_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          postSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCardStatus.POST_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          postSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCardStatus.POST_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          postSingle: {
            isFetching: false,
            didInvalidate: false,
            error: action.data
          }
        }
      });
    default:
      return state;
  }
}


function deleteSingle(state, action) {
  switch (action.type) {
    case Constants.creditCardStatus.DELETE_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          deleteSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCardStatus.DELETE_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          deleteSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCardStatus.DELETE_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          deleteSingle: {
            isFetching: false,
            didInvalidate: false,
            error: action.data
          }
        }
      });
    default:
      return state;
  }
}


function updateSingle(state, action) {
  switch (action.type) {
    case Constants.creditCardStatus.UPDATE_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          updateSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCardStatus.UPDATE_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          updateSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCardStatus.UPDATE_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCardStatus: {
          ...state.creditCardStatus,
          updateSingle: {
            isFetching: false,
            didInvalidate: false,
            error: action.data
          }
        }
      });
    default:
      return state;
  }
}
