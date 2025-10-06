import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import App from "./App";
import LoginForm from "./LoginForm";
import RegisterForm from "./RegisterForm";
import React, { useState } from "react";
import { NavBar } from "./NavBar/NavBar";
import ProtectedRoute from "./ProtectedRoute";

export type User = {
  id: number;
  email: string;
  username: string;
};

function MainRouter() {
  const [user, setUser] = useState<null | User>(null);

  return (
    <Router>
      <Routes>
        <Route
          path="/"
          element={
            <ProtectedRoute user={user}>
              <App user={user} setUser={setUser} />
            </ProtectedRoute>
          }
        />
        <Route
          path="/login"
          element={
            <>
              <NavBar user={user} onLogout={() => setUser(null)} />
              <LoginForm onLogin={setUser} />
            </>
          }
        />
        <Route
          path="/register"
          element={
            <>
              <NavBar user={user} onLogout={() => setUser(null)} />
              <RegisterForm onRegister={setUser} />
            </>
          }
        />
      </Routes>
    </Router>
  );
}

export default MainRouter;
