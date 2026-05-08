type PlaceholderPageProps = {
  title: string;
};

export function PlaceholderPage({ title }: PlaceholderPageProps) {
  return (
    <section className="placeholder-page">
      <p className="eyebrow">Temporary Page</p>
      <h1>{title}</h1>
      <p>
        This module is ready in the menu. Add the form or list here when the API
        contract is finalized.
      </p>
    </section>
  );
}
