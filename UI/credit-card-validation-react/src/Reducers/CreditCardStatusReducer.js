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
          isFetching: false,
          didInvalidate: false,
          error: action.data
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