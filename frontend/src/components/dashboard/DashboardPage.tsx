export function DashboardPage() {
  return (
    <section className="dashboard-grid">
      <div className="section-heading dashboard-title">
        <p className="eyebrow">Overview</p>
        <h1>Dashboard</h1>
      </div>

      <div className="erp-card schedule-card">
        <div className="card-title">
          <div>
            <h2>Things To Do</h2>
            <span>Today&apos;s events</span>
          </div>
          <button type="button">+ New To Do</button>
        </div>

        <div className="calendar-strip">
          {["Mon", "Tue", "Wed", "Thu", "Fri", "Sat"].map((day, index) => (
            <div className={index === 1 ? "selected" : ""} key={day}>
              <strong>{21 + index}</strong>
              <span>{day}</span>
            </div>
          ))}
        </div>

        <div className="timeline">
          <div style={{ gridColumn: "2 / span 2" }}>Admission review</div>
          <div style={{ gridColumn: "4 / span 2" }}>Document verification</div>
          <div style={{ gridColumn: "3 / span 2" }}>User access audit</div>
          <div style={{ gridColumn: "5 / span 2" }}>Employee onboarding</div>
        </div>
      </div>

      <div className="erp-card compact-card">
        <div className="card-title">
          <h2>Compliance</h2>
          <span>More</span>
        </div>
        <p>
          <strong>5 new</strong> records are due for review this week.
        </p>
        <p>
          <strong>3 documents</strong> are waiting for approval.
        </p>
      </div>

      <div className="erp-card news-card">
        <div className="card-title">
          <h2>Company News</h2>
          <button type="button">View All</button>
        </div>
        <ul>
          <li>New user registration flow is connected to the API.</li>
          <li>Use the sidebar to open the user list and temporary modules.</li>
          <li>File, student, and employee modules can be added next.</li>
        </ul>
      </div>

      <div className="erp-card engagement-card">
        <div className="card-title">
          <h2>Engagement</h2>
          <button type="button">View All</button>
        </div>
        <p>
          Most active area today is <strong>User Management</strong>.
        </p>
        <div className="message-pill">3 Messages</div>
      </div>
    </section>
  );
}
