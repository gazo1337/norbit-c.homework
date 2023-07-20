import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../hooks/redux';
import { taskAPI } from '../services/TaskService';
import { fetchTasks } from '../store/reducers/ActionCreators';
import TaskItem from "./TaskItem"
import '../styles/Menu.css';

const TaskMenu = () => {
    /*const dispatch = useAppDispatch();
    const { tasks } = useAppSelector(state => state.userReducer)

    useEffect(() => {
        dispatch(fetchTasks())
    }, [ ])*/
    const { data: tasks, error, isLoading } = taskAPI.useFetchAllTasksQuery(1)
    return (
        <div className = "Menu">
            <header className="Our-header">
                <p>NFreelance</p>
            </header>
            <div className="Content">
                <h1>Доступные задания</h1>
                {isLoading && <h2>Идёт загрузка...</h2>}
                {error && <h2>Произошла ошибка при загрузке данных :(</h2> }
                {tasks && tasks.map(task =>
                    <TaskItem task={task} />
                )}
            </div>
        </div>
    )
}

export default TaskMenu