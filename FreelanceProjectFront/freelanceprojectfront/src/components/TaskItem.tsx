import React, { FC } from 'react';
import { ITask } from '../models/ITask';

interface TaskItemProps {
    task: ITask;
}

const TaskItem: FC<TaskItemProps> = ({ task }) => {
    return (
        <div>
            <h2>{task.taskName}</h2>
            <p>{task.taskDescript}</p>
        </div>
    );
}

export default TaskItem;