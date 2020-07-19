import { Constants } from "../Constants";
import objectPath from "object-path";

export function CreditCardStatusReducer(state = {
  creditCardStatus: {
    fetchAll: {
      isFetching: false,
      didInvalidate: false,
      results: []
    }
  }
}, action) {
  try {
    switch (action.type) {
      case Constants.creditCardStatus.GET_CREDIT_CARD_STATUS:
        return state.creditCardStatus.list;
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
  }
}

// function getCreditCardStatus() {
//   try {
//     const response = fetch("http://localhost:6003/api/creditCardStatus").then(res => res.json());
//     console.log("Credit Card Statuses", response);
//     return response;
//   } catch (err) {
//     console.log(err);
//     throw err;
//   }
// }