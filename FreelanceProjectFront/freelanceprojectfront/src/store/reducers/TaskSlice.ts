import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { ITask } from "../../models/ITask";
import { IUser } from "../../models/IUser";


export interface TaskState {
    tasks: ITask[];
    isLoading: boolean;
    error: string;
}

const initialState: TaskState = {
    tasks: [],
    isLoading: false,
    error: '',
}

export const TaskSlice = createSlice({
    name: 'task',
    initialState,
    reducers: {
        tasksFetching(state) {
            state.isLoading = true;
        },
        tasksFetchingSuccess(state, action: PayloadAction<ITask[]>) {
            state.isLoading = false;
            state.error = '';
            state.tasks = action.payload;
        },
        tasksFetchingError(state, action: PayloadAction<string>) {
            state.isLoading = false;
            state.error = action.payload;
        }
    }
});

export default TaskSlice.reducer;