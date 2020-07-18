import { Constants } from "../Constants";
import objectPath from "object-path";

export function CreditCardProviderReducer(state, action) {
  objectPath.ensureExists(state, "creditCardProvider", {
    list: []
  });
  switch (action.type) {
    case Constants.creditCardProvider.GET_CREDIT_CARD_PROVIDER:
      return state.creditCardProvider.list;
    case Constants.creditCardProvider.UPDATE_CREDIT_CARD_PROVIDER:
      return Object.assign({}, state, {
        creditCardProvider: {
          list: state.creditCardProvider.list.push("11")
        }
      });
    case Constants.creditCardProvider.ADD_CREDIT_CARD_PROVIDER:
      return Object.assign({}, state, {
        creditCardProvider: {
          list: state.creditCardProvider.list.push("7")
        }
      });
    case Constants.creditCardProvider.RELOAD:
      return Object.assign({}, state, {
        creditCardProvider: {
          list: ["AMC", "AMD", "AFR"]
        }
      });
    default:
      return state;
  }
}