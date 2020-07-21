import { Constants } from "../Constants";
import objectPath from "object-path";

export function CreditCardProviderReducer(state, action) {
  try {
    objectPath.ensureExists(state, "creditCardProvider.pagination", { pageNumber: 1, pageSize: 10 })
    switch (action.type) {
      case Constants.creditCardProvider.SET_GRID_PAGINATION:
        return paginationPagination(state, action);
      case Constants.creditCardProvider.RELOAD:
        return Object.assign({}, state, {
          creditCardProvider: {
            ...state.creditCardProvider,
            reload: true
          }
        });
      case Constants.creditCardProvider.FETCH_ALL_REQUEST:
      case Constants.creditCardProvider.FETCH_ALL_SUCCESS:
      case Constants.creditCardProvider.FETCH_ALL_FAILURE:
        return fetchAll(state, action);
      case Constants.creditCardProvider.FETCH_SINGLE_REQUEST:
      case Constants.creditCardProvider.FETCH_SINGLE_SUCCESS:
      case Constants.creditCardProvider.FETCH_SINGLE_FAILURE:
        return fetchSingle(state, action);
      case Constants.creditCardProvider.POST_SINGLE_REQUEST:
      case Constants.creditCardProvider.POST_SINGLE_SUCCESS:
      case Constants.creditCardProvider.POST_SINGLE_FAILURE:
        return postSingle(state, action);
      case Constants.creditCardProvider.DELETE_SINGLE_REQUEST:
      case Constants.creditCardProvider.DELETE_SINGLE_SUCCESS:
      case Constants.creditCardProvider.DELETE_SINGLE_FAILURE:
        return deleteSingle(state, action);
      case Constants.creditCardProvider.UPDATE_SINGLE_REQUEST:
      case Constants.creditCardProvider.UPDATE_SINGLE_SUCCESS:
      case Constants.creditCardProvider.UPDATE_SINGLE_FAILURE:
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
    case Constants.creditCardProvider.FETCH_ALL_REQUEST:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
          fetchAll: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCardProvider.FETCH_ALL_SUCCESS:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
          fetchAll: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCardProvider.FETCH_ALL_FAILURE:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
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
  const page = action.data && action.data.pageNumber ? action.data.pageNumber : state.creditCardProvider.pagination.pageNumber;
  const pageSize = action.data && action.data.pageSize ? action.data.pageSize : state.creditCardProvider.pagination.pageSize;
  const totalCount = action.data && action.data.totalCount ? action.data.totalCount : 0;
  return Object.assign({}, state, {
    creditCardProvider: {
      ...state.creditCardProvider,
      pagination: {
        pageNumber: page,
        pageSize: pageSize,
        totalCount: totalCount
      }
    }
  });
}

function fetchSingle(state, action) {
  switch (action.type) {
    case Constants.creditCardProvider.FETCH_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
          fetchSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCardProvider.FETCH_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
          fetchSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCardProvider.FETCH_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
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
    case Constants.creditCardProvider.POST_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
          postSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCardProvider.POST_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
          postSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCardProvider.POST_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
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
    case Constants.creditCardProvider.DELETE_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
          deleteSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCardProvider.DELETE_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
          deleteSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCardProvider.DELETE_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
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
    case Constants.creditCardProvider.UPDATE_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
          updateSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCardProvider.UPDATE_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
          updateSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCardProvider.UPDATE_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCardProvider: {
          ...state.creditCardProvider,
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
