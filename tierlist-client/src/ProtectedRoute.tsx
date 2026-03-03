import React, { JSX } from "react";
import { Navigate } from "react-router-dom";
import { User } from "./MainRouter";

interface ProtectedRouteProps {
  user: User | null;
  children: JSX.Element;
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({ user, children }) => {
  if (!user) {
    return <Navigate to="/login" replace />;
  }

  return children;
};

export default ProtectedRoute;