import React from 'react';
import logo from './logo.svg';
import './App.css';
import './css/bootstrap.min.css'
import {TestFormSchema} from "./TestFormSchema/TestFormSchema";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <button className="btn btn-success">Hello</button>
      </header>
        <TestFormSchema />
    </div>
  );
}

export default App;
