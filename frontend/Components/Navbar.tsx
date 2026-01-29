"use client";
import React, { useState } from "react";
import { Gamepad2, SearchIcon } from "lucide-react";
import Button from "./Button";

const Navbar = () => {
  const [isSearchOpen, setIsSearchOpen] = useState(false);

  return (
    <nav className="w-full p-4">
      <div className="flex items-center justify-between gap-4">
        {/* Logo */}
        <h1 className="text-xl md:text-2xl text-red-600 font-bold flex items-center gap-1 shrink-0">
          <Gamepad2 size={24} />
          <div>
            <span className="text-white">Game</span>Store
          </div>
        </h1>

        {/* Search Bar - Desktop */}
        <div className="hidden md:flex max-w-sm w-full relative">
          <input
            type="search"
            placeholder="Cari game atau developer"
            className="w-full bg-gray-700 px-3 py-2 rounded-md text-base text-white placeholder:text-gray-400 focus:outline-none focus:ring-2 focus:ring-red-500"
          />
          <SearchIcon
            size={20}
            className="absolute right-3 top-2.5 text-gray-400"
          />
        </div>

        {/* Action Buttons - Desktop */}
        <div className="hidden md:flex items-center gap-3 shrink-0">
          <Button href="/" variant="success" size="medium">
            Daftar
          </Button>
          <Button href="/login" variant="primary" size="medium">
            Masuk
          </Button>
        </div>

        {/* Mobile Actions */}
        <div className="flex md:hidden items-center gap-2">
          <button
            onClick={() => setIsSearchOpen(!isSearchOpen)}
            className="p-2 text-white hover:bg-gray-700 rounded-md transition-colors"
            aria-label="Toggle search"
          >
            <SearchIcon size={20} />
          </button>
          <Button href="/login" variant="primary" size="small">
            Masuk
          </Button>
        </div>
      </div>

      {/* Search Bar - Mobile (Expandable) */}
      {isSearchOpen && (
        <div className="md:hidden mt-4 relative">
          <input
            type="search"
            placeholder="Cari game atau developer"
            className="w-full bg-gray-700 px-3 py-2 rounded-md text-base text-white placeholder:text-gray-400 focus:outline-none focus:ring-2 focus:ring-red-500"
          />
          <SearchIcon
            size={20}
            className="absolute right-3 top-2.5 text-gray-400"
          />
        </div>
      )}
    </nav>
  );
};

export default Navbar;
