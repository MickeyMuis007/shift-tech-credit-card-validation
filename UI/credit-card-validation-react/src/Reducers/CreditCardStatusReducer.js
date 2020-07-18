import { Constants } from "../Constants";
import objectPath from "object-path";

export function CreditCardStatusReducer(state, action) {
  objectPath.ensureExists(state, "creditCardStatus", { list: []});

  switch (action.type) {
    case Constants.creditCardStatus.GET_CREDIT_CARD_STATUS:
      return state.creditCardStatus.list;
    case Constants.creditCardStatus.RELOAD:
      return Object.assign({}, state, {
        creditCardStatus: {
          list: [1,2,5,6,7]
        }
      })
    default:
      return state;
  }
}