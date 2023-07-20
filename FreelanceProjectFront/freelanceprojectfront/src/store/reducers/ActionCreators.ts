import axios from "axios"
import { ITask } from "../../models/ITask"
import { AppDispatch } from "../store"
import { TaskSlice } from "./TaskSlice"


export const fetchTasks = () => async (dispatch: AppDispatch) => {
    try {
        dispatch(TaskSlice.actions.tasksFetching())
        const responce = await axios.get<ITask[]>('http://localhost:5556/api/tasks/all')
        dispatch(TaskSlice.actions.tasksFetchingSuccess(responce.data))
    }
    catch (e:any) {
        dispatch(TaskSlice.actions.tasksFetchingError(e.message))
    }
}