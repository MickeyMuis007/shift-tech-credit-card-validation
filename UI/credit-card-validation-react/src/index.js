import React from 'react';
import ReactDOM from 'react-dom';
import * as Redux from "redux";
import * as ReactRedux from "react-redux";
import { MainConstants } from "./Constants";
import * as Reducers from "./Reducers";
import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import 'bootstrap/dist/css/bootstrap.min.css';
import '@fortawesome/fontawesome-free/css/all.min.css'; 
import 'bootstrap-css-only/css/bootstrap.min.css'; 
import 'mdbreact/dist/css/mdb.css';

function reducer(state = {}, action) {
  switch (caseSplit(action.type)) {
    case MainConstants.LOGIN:
      return Reducers.LoginReducer(state, action);
    case MainConstants.CREDIT_CARD:
      return Reducers.CreditCardReducer(state, action);
    case MainConstants.CREDIT_CARD_PROVIDER:
      return Reducers.CreditCardProviderReducer(state, action);
    case MainConstants.CREDIT_CARD_STATUS:
      return Reducers.CreditCardStatusReducer(state, action);
    default:
      return state;
  }
}

function caseSplit(actionType) {
  if (!actionType && !actionType instanceof String) return "";

  return actionType.split("-")[0] || "";
}

let store = Redux.createStore(
  reducer,
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

ReactDOM.render(
  <React.StrictMode>
    <ReactRedux.Provider store={store} >
      <App />
    </ReactRedux.Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
