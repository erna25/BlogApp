import React from 'react'
import ReactDOM from 'react-dom/client'
import { Provider } from 'react-redux'
import './index.css'
import App from './App.js'
import configureStore from './store/configureStore.jsx'

const store = configureStore();

const root = ReactDOM.createRoot(document.getElementById('content'));
root.render(
    <Provider store={store}>
        <App />
    </Provider>
);
