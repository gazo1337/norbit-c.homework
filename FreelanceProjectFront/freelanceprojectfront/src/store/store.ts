import { combineReducers, configureStore, getDefaultMiddleware } from "@reduxjs/toolkit";
import { type } from "os";
import { taskAPI } from "../services/TaskService";
import userReducer from './reducers/TaskSlice'

const rootReducer = combineReducers({
    userReducer,
    [taskAPI.reducerPath]: taskAPI.reducer
})

export const setupStore = () => {
    return configureStore({
        reducer: rootReducer,
        middleware: (getDefaultMiddleware) =>
            getDefaultMiddleware().concat(taskAPI.middleware)
    })
}

export type RootState = ReturnType<typeof rootReducer>
export type AppStore = ReturnType<typeof setupStore>
export type AppDispatch = AppStore['dispatch']