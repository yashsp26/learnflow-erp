"use client";

import { FormEvent, useState } from "react";
import { createUser } from "@/lib/users-api";
import { useAuth } from "@/context/auth-context";

export function UserRegistrationPage() {
  const { session } = useAuth();
  const [registrationForm, setRegistrationForm] = useState({
    username: "",
    email: "",
    password: "",
    roleId: "1",
  });
  const [registrationMessage, setRegistrationMessage] = useState("");
  const [isCreatingUser, setIsCreatingUser] = useState(false);

  async function handleCreateUser(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    if (!session?.token) {
      return;
    }

    setRegistrationMessage("");
    setIsCreatingUser(true);

    try {
      const data = await createUser(session.token, {
        username: registrationForm.username,
        roleId: Number(registrationForm.roleId),
        email: registrationForm.email,
        password: registrationForm.password,
      });

      setRegistrationMessage(`User created successfully. User ID: ${data.Data}`);
      setRegistrationForm({
        username: "",
        email: "",
        password: "",
        roleId: "1",
      });
    } catch (error) {
      setRegistrationMessage(
        error instanceof Error ? error.message : "Unable to create user.",
      );
    } finally {
      setIsCreatingUser(false);
    }
  }

  return (
    <section className="page-grid">
      <div className="section-heading">
        <p className="eyebrow">Access Control</p>
        <h1>User Registration</h1>
      </div>

      <form className="erp-card form-card" onSubmit={handleCreateUser}>
        <label>
          Full Name
          <input
            value={registrationForm.username}
            onChange={(event) =>
              setRegistrationForm({
                ...registrationForm,
                username: event.target.value,
              })
            }
            placeholder="Yash Bhau"
            required
          />
        </label>

        <label>
          Email
          <input
            type="email"
            value={registrationForm.email}
            onChange={(event) =>
              setRegistrationForm({
                ...registrationForm,
                email: event.target.value,
              })
            }
            placeholder="user@college.edu"
            required
          />
        </label>

        <label>
          Password
          <input
            type="password"
            value={registrationForm.password}
            onChange={(event) =>
              setRegistrationForm({
                ...registrationForm,
                password: event.target.value,
              })
            }
            placeholder="Temporary password"
            required
          />
        </label>

        <label>
          Role ID
          <input
            type="number"
            min="1"
            value={registrationForm.roleId}
            onChange={(event) =>
              setRegistrationForm({
                ...registrationForm,
                roleId: event.target.value,
              })
            }
            required
          />
        </label>

        {registrationMessage ? (
          <p className="form-status">{registrationMessage}</p>
        ) : null}

        <button
          type="submit"
          className="primary-button"
          disabled={isCreatingUser}
        >
          {isCreatingUser ? "Creating..." : "Create User"}
        </button>
      </form>
    </section>
  );
}
