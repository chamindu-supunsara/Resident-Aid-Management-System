describe('Login (UI to API)', () => {
  it('submits credentials, calls backend, and lands on dashboard', () => {
    const email = Cypress.env('LOGIN_EMAIL') as string;
    const password = Cypress.env('LOGIN_PASSWORD') as string;

    if (!email || !password) {
      throw new Error(
        'Set CYPRESS_LOGIN_EMAIL and CYPRESS_LOGIN_PASSWORD (e.g. GitHub Actions secrets E2E_LOGIN_EMAIL / E2E_LOGIN_PASSWORD).'
      );
    }

    cy.intercept('POST', '**/api/Login/Login').as('loginApi');

    cy.visit('/');
    cy.get('#login-username').should('be.visible').clear().type(email);
    cy.get('#login-password').should('be.visible').clear().type(password, { log: false });
    cy.contains('button', 'Sign In').should('be.visible').click();

    cy.wait('@loginApi', { timeout: 30000 }).then((interception) => {
      expect(interception.response?.statusCode, 'login HTTP status').to.eq(200);
      const body = interception.response?.body as { success?: boolean };
      expect(body?.success, 'login API success').to.eq(true);
    });

    cy.url({ timeout: 30000 }).should('include', '/dashboard');
  });
});
