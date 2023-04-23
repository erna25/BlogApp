import React from 'react'
import './App.css'
import { BrowserRouter as Router } from 'react-router-dom'
import Routing from './routes/route.jsx'
import Header from './containers/header/header.jsx'

function App() {
    return (
        <Router basename="Blog">
            <div>
                <Header />  
                <Routing />                
            </div>
        </Router>
  );
}

export default App;
