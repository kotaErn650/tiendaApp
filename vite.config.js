import { defineConfig } from 'vite';
import os from 'os';

function getLocalIP() {
  const interfaces = os.networkInterfaces();
  for (const name of Object.keys(interfaces)) {
    for (const iface of interfaces[name]) {
      if (iface.family === 'IPv4' && !iface.internal) {
        return iface.address;
      }
    }
  }
  return 'localhost';
}

export default defineConfig({
  server: {
    host: '0.0.0.0',
    port: 5173,
  },
  preview: {
    host: '0.0.0.0',
    port: 4173,
  },
  plugins: [
    {
      name: 'print-network-url',
      configureServer(server) {
        server.httpServer?.once('listening', () => {
          const ip = getLocalIP();
          const port = server.config.server.port ?? 5173;
          console.log('\n  📱  Desde tu celular Android (misma red WiFi):');
          console.log(`      http://${ip}:${port}\n`);
        });
      },
    },
  ],
});
