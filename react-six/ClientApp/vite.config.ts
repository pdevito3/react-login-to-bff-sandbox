// import reactRefresh from '@vitejs/plugin-react-refresh';
// import { defineConfig } from 'vite';

// // https://vitejs.dev/config/

// const path = require('path');

// export default defineConfig({
//   resolve: {
//     alias: {
//       '@': path.resolve(__dirname, '/src')
//     }
//   },
//   plugins: [reactRefresh()]
// })

import reactRefresh from '@vitejs/plugin-react-refresh';
import { readFileSync } from 'fs';
import { join } from 'path';
import { defineConfig } from 'vite';

const baseFolder =
  process.env.APPDATA !== undefined && process.env.APPDATA !== ''
    ? `${process.env.APPDATA}/ASP.NET/https`
    : `${process.env.HOME}/.aspnet/https`;

const certificateName = process.env.npm_package_name

const certFilePath = join(baseFolder, `${certificateName}.pem`);
const keyFilePath = join(baseFolder, `${certificateName}.key`);

const { env } = require("process");

const target = env.ASPNETCORE_HTTPS_PORT
    ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
    : env.ASPNETCORE_URLS
        ? env.ASPNETCORE_URLS.split(";")[0]
        : "http://localhost:18082";

const baseProxy = {
  target,
  secure: false
};

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [reactRefresh()],
  server: {
    https: {
      key: readFileSync(keyFilePath),
      cert: readFileSync(certFilePath)
    },
    port: 44434,
    strictPort: true,
    proxy: {
      '/bff': baseProxy,
      '/signin-oidc': baseProxy,
      '/signout-callback-oidc': baseProxy
    }
  }
})