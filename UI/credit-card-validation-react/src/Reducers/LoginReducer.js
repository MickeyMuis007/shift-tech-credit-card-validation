import { Constants } from "../Constants";

export function LoginReducer(state, action) {
  switch (action.type) {
    case Constants.login.AUTHENTICATE: 
      return Object.assign({}, state, { login: { isLoggedIn: true }});
    default:
      return state;
  }
}