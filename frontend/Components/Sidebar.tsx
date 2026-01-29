"use client";
import React from "react";
import {
  HomeIcon,
  ChartBarStacked,
  DownloadIcon,
  SettingsIcon,
} from "lucide-react";
import Link from "next/link";
import { usePathname } from "next/navigation";

const Sidebar = () => {
  const pathname = usePathname();

  const navLinks = [
    { name: "Home", href: "/", icon: HomeIcon },
    { name: "Kategori", href: "/categories", icon: ChartBarStacked },
    { name: "Downloads", href: "/downloads", icon: DownloadIcon },
    { name: "Settings", href: "/settings", icon: SettingsIcon },
  ];

  return (
    <>
      {/* Desktop Sidebar - Fixed Position */}
      <aside className="hidden md:block fixed left-0 top-[73px] w-56 h-[calc(100vh-73px)] p-3 bg-gray-800 border-r border-gray-600 z-40 overflow-y-auto">
        <div className="flex flex-col gap-2">
          {navLinks.map((link, index) => {
            const isActive = pathname === link.href;
            return (
              <Link
                key={index}
                href={link.href}
                className={`w-full flex items-center gap-3 text-base font-medium px-4 py-3
                  text-white rounded-lg transition-all duration-200 
                  ${
                    isActive
                      ? "bg-gradient-to-r from-red-600 to-orange-500 shadow-md"
                      : "hover:bg-gray-700"
                  }`}
              >
                <link.icon size={20} />
                <span>{link.name}</span>
              </Link>
            );
          })}
        </div>
      </aside>

      {/* Mobile Bottom Navigation - Fixed Position */}
      <nav className="md:hidden fixed bottom-0 left-0 right-0 bg-gray-800 border-t border-gray-600 z-50">
        <div className="flex items-center justify-around p-2">
          {navLinks.map((link, index) => {
            const isActive = pathname === link.href;
            return (
              <Link
                key={index}
                href={link.href}
                className={`flex flex-col items-center gap-1 px-3 py-2 rounded-lg transition-all duration-200 min-w-[70px]
                  ${
                    isActive ? "text-red-500" : "text-gray-400 hover:text-white"
                  }`}
              >
                <link.icon size={22} />
                <span className="text-xs font-medium">{link.name}</span>
              </Link>
            );
          })}
        </div>
      </nav>
    </>
  );
};

export default Sidebar;
