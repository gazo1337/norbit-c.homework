import React from 'react';
import logo from './logo.svg';
import './App.css';
import Login from './components/Login';
import TaskMenu from './components/TasksMenu'
import UserMenu from './components/UserMenu'
import { BrowserRouter, Routes, Route } from 'react-router-dom';

function App() {
    return (
        <BrowserRouter>
            <div className="App">
                <Routes>
                    <Route path="/" element={<Login />} />
                    <Route path="/tasks" element={<TaskMenu/> }/>
                </Routes>
            </div>
        </BrowserRouter>
  );
}

export default App;
