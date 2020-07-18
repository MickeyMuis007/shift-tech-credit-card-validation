import { Constants } from "../Constants";
import objectPath from "object-path";

export function CreditCardReducer(state, action) {
  objectPath.ensureExists(state, "creditCard", { list: [] });

  switch (action.type) {
    case Constants.creditCard.GET_CREDIT_CARD:
      return state.creditCard.list;
    case Constants.creditCard.ADD_CREDIT_CARD:
      return Object.assign({}, state, {
        creditCard: {
          list: state.creditCard.list.push("7")
        }
      });
    case Constants.creditCard.RELOAD:
      return Object.assign({}, state, {
        creditCard: {
          list: ["0704", "87934", "117"]
        }
      });
    default:
      return state;
  }
}