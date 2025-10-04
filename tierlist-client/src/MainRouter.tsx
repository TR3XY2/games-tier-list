import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import App from "./App";
import LoginForm from "./LoginForm";
import RegisterForm from "./RegisterForm";
import React, { useState } from "react";
import { NavBar } from "./NavBar/NavBar";

function MainRouter() {
  const [user, setUser] = useState<null | {
    id: number;
    email: string;
    username: string;
  }>(null);

  return (
    <Router>
      <Routes>
        <Route path="/" element={<App />} />
        <Route
          path="/login"
          element={
            <>
              <NavBar />
              <LoginForm onLogin={setUser} />
            </>
          }
        />
        <Route
          path="/register"
          element={
            <>
              <NavBar />
              <RegisterForm onRegister={setUser} />
            </>
          }
        />
      </Routes>
    </Router>
  );
}

export default MainRouter;
