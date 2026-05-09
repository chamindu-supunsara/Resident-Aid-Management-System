import { defineConfig } from 'cypress';

export default defineConfig({
  e2e: {
    supportFile: false,
    video: false,
    specPattern: 'cypress/e2e/**/*.cy.ts',
    baseUrl: process.env.CYPRESS_BASE_URL || 'http://127.0.0.1:5087',
    env: {
      LOGIN_EMAIL: process.env.CYPRESS_LOGIN_EMAIL ?? '',
      LOGIN_PASSWORD: process.env.CYPRESS_LOGIN_PASSWORD ?? '',
    },
  },
});
