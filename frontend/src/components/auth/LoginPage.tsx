"use client";

import { FormEvent, useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/context/auth-context";

export function LoginPage() {
  const router = useRouter();
  const { login, session } = useAuth();
  const [loginForm, setLoginForm] = useState({
    email: "yashsachinpatil2662@gmail.com",
    password: "1234",
    tenantCode: "CLG001",
  });
  const [loginError, setLoginError] = useState("");
  const [isLoggingIn, setIsLoggingIn] = useState(false);

  useEffect(() => {
    if (session) {
      router.push("/dashboard");
    }
  }, [router, session]);

  async function handleLogin(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    setLoginError("");
    setIsLoggingIn(true);

    try {
      await login(loginForm);
      router.push("/dashboard");
    } catch (error) {
      setLoginError(
        error instanceof Error
          ? error.message
          : "Unable to login. Check API server and credentials.",
      );
    } finally {
      setIsLoggingIn(false);
    }
  }

  return (
    <main className="login-screen">
      <section className="login-panel" aria-label="Login">
        <div className="brand-mark">
          <span>LF</span>
          <small>LearnFlow ERP</small>
        </div>

        <div className="login-copy">
          <p className="eyebrow">Institution Portal</p>
          <h1>Welcome back</h1>
          <p>
            Sign in with your tenant account to continue to the ERP dashboard.
          </p>
        </div>

        <form className="login-form" onSubmit={handleLogin}>
          <label>
            Email
            <input
              type="email"
              value={loginForm.email}
              onChange={(event) =>
                setLoginForm({ ...loginForm, email: event.target.value })
              }
              placeholder="admin@college.edu"
              required
            />
          </label>

          <label>
            Password
            <input
              type="password"
              value={loginForm.password}
              onChange={(event) =>
                setLoginForm({ ...loginForm, password: event.target.value })
              }
              placeholder="Enter password"
              required
            />
          </label>

          <label>
            Tenant Code
            <input
              value={loginForm.tenantCode}
              onChange={(event) =>
                setLoginForm({ ...loginForm, tenantCode: event.target.value })
              }
              placeholder="CLG001"
              required
            />
          </label>

          {loginError ? <p className="form-error">{loginError}</p> : null}

          <button type="submit" className="primary-button" disabled={isLoggingIn}>
            {isLoggingIn ? "Signing in..." : "Login"}
          </button>
        </form>
      </section>

      <section className="login-visual" aria-label="ERP overview preview">
        <div className="visual-card wide">
          <span>Today</span>
          <strong>8 active modules</strong>
          <p>Users, students, employees, documents, and daily operations.</p>
        </div>
        <div className="visual-row">
          <div className="visual-card">
            <span>Users</span>
            <strong>Role based</strong>
          </div>
          <div className="visual-card">
            <span>API</span>
            <strong>Connected</strong>
          </div>
        </div>
      </section>
    </main>
  );
}
