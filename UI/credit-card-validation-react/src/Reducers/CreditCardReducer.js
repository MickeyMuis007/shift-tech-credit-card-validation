import { Constants } from "../Constants";
import objectPath from "object-path";

export function CreditCardReducer(state, action) {
  try {
    objectPath.ensureExists(state, "creditCard.pagination", { pageNumber: 1, pageSize: 10 })
    switch (action.type) {
      case Constants.creditCard.SET_GRID_PAGINATION:
        return paginationPagination(state, action);
      case Constants.creditCard.RELOAD:
        return Object.assign({}, state, {
          creditCard: {
            ...state.creditCard,
            reload: true
          }
        });
      case Constants.creditCard.FETCH_ALL_REQUEST:
      case Constants.creditCard.FETCH_ALL_SUCCESS:
      case Constants.creditCard.FETCH_ALL_FAILURE:
        return fetchAll(state, action);
      case Constants.creditCard.FETCH_SINGLE_REQUEST:
      case Constants.creditCard.FETCH_SINGLE_SUCCESS:
      case Constants.creditCard.FETCH_SINGLE_FAILURE:
        return fetchSingle(state, action);
      case Constants.creditCard.POST_SINGLE_REQUEST:
      case Constants.creditCard.POST_SINGLE_SUCCESS:
      case Constants.creditCard.POST_SINGLE_FAILURE:
        return postSingle(state, action);
      case Constants.creditCard.DELETE_SINGLE_REQUEST:
      case Constants.creditCard.DELETE_SINGLE_SUCCESS:
      case Constants.creditCard.DELETE_SINGLE_FAILURE:
        return deleteSingle(state, action);
      case Constants.creditCard.UPDATE_SINGLE_REQUEST:
      case Constants.creditCard.UPDATE_SINGLE_SUCCESS:
      case Constants.creditCard.UPDATE_SINGLE_FAILURE:
        return updateSingle(state, action);
      case Constants.creditCard.VALIDATE_NO_REQUEST:
      case Constants.creditCard.VALIDATE_NO_SUCCESS:
      case Constants.creditCard.VALIDATE_NO_FAILURE:
        return validateCreditCardNo(state, action);
      default:
        return state;
    }
  } catch (err) {
    return state;
  }
}

function fetchAll(state, action) {
  switch (action.type) {
    case Constants.creditCard.FETCH_ALL_REQUEST:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          fetchAll: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCard.FETCH_ALL_SUCCESS:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          fetchAll: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCard.FETCH_ALL_FAILURE:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
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
  const page = action.data && action.data.pageNumber ? action.data.pageNumber : state.creditCard.pagination.pageNumber;
  const pageSize = action.data && action.data.pageSize ? action.data.pageSize : state.creditCard.pagination.pageSize;
  const totalCount = action.data && action.data.totalCount ? action.data.totalCount : 0;
  return Object.assign({}, state, {
    creditCard: {
      ...state.creditCard,
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
    case Constants.creditCard.FETCH_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          fetchSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCard.FETCH_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          fetchSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCard.FETCH_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
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
    case Constants.creditCard.POST_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          postSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCard.POST_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          postSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCard.POST_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
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
    case Constants.creditCard.DELETE_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          deleteSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCard.DELETE_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          deleteSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCard.DELETE_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
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
    case Constants.creditCard.UPDATE_SINGLE_REQUEST:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          updateSingle: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCard.UPDATE_SINGLE_SUCCESS:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          updateSingle: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCard.UPDATE_SINGLE_FAILURE:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
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

function validateCreditCardNo(state, action) {
  switch (action.type) {
    case Constants.creditCard.VALIDATE_NO_REQUEST:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          validateCreditCardNo: {
            isFetching: true,
            didInvalidate: false
          }
        }
      })
    case Constants.creditCard.VALIDATE_NO_SUCCESS:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          validateCreditCardNo: {
            isFetching: false,
            didInvalidate: false,
            results: action.data
          }
        }
      });
    case Constants.creditCard.VALIDATE_NO_FAILURE:
      return Object.assign({}, state, {
        creditCard: {
          ...state.creditCard,
          validateCreditCardNo: {
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