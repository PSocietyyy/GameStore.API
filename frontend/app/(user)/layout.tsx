import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "../globals.css";
import Navbar from "@/Components/Navbar";
import Sidebar from "@/Components/Sidebar";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "GameStore - Platform Game Terbaik",
  description: "Temukan dan download game favorit kamu",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="id">
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased min-h-screen bg-gray-800`}
      >
        {/* Fixed Navbar */}
        <div className="fixed top-0 left-0 right-0 z-50 bg-gray-800 border-b border-gray-600">
          <Navbar />
        </div>

        {/* Sidebar - Fixed on desktop, bottom nav on mobile */}
        <Sidebar />

        {/* Main Content with proper spacing */}
        <main className="pt-[73px] md:ml-56 p-4 md:p-6 pb-20 md:pb-6 min-h-screen">
          <div className="max-w-7xl mx-auto w-full">{children}</div>
        </main>
      </body>
    </html>
  );
}
