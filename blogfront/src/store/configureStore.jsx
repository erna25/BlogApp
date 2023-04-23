import { createStore, applyMiddleware } from 'redux'
import rootReducer from '../reducers/rootReducer.jsx'
import thunk from 'redux-thunk'

export default function configureStore(initialState) {
    const store = createStore(rootReducer, initialState, applyMiddleware(thunk));

    var context = require.context('../reducers');
    if (module.hot) {
        module.hot.accept(context, () => {
            const nextRootReducer = require.context('../reducers');
            store.replaceReducer(nextRootReducer);
        })
    }

    return store
}