import React, { useState } from 'react';
import '../App.css';

const Login: React.FC = () => {
    return (
        <div className = "App.css">
            <header className="App-header">
                <p>NFreelance</p>
            </header>
            <div className="App-body">
                <p className="App-body1">Вход</p>
                <form >
                    <p><input type="text" id="login" placeholder="Логин" /></p>
                    <p><input type="password" id="password" placeholder="Пароль" /></p>
                    <p><button className="App-button">Войти</button></p>
                </form>
            </div>
        </div>
    )
}

export default Login