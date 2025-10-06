import React from "react";
import { Link } from "react-router-dom";
import { Logo } from "./Logo";

interface NavBarProps {
  user: null | { id: number; email: string; username: string };
  onLogout: () => void;
}

export function NavBar({ user, onLogout }: NavBarProps) {
  return (
    <nav className="nav-bar">
      <Logo />
      <div className="nav-links">
        {user ? (
          <>
            <span>Welcome, {user.username}</span>
            <button className="logout-btn" onClick={onLogout}>
              Log Out
            </button>
          </>
        ) : (
          <>
            <Link to="/login" className="nav-link">
              Log In
            </Link>
            <Link to="/register" className="nav-link">
              Sign Up
            </Link>
          </>
        )}
      </div>
    </nav>
  );
}