"use client";
import Link from "next/link";
import React from "react";

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  children: React.ReactNode;
  variant?:
    | "primary"
    | "secondary"
    | "success"
    | "danger"
    | "warning"
    | "outline";
  size?: "small" | "medium" | "large";
  href?: string;
}

const Button = ({
  children,
  variant = "primary",
  size = "medium",
  href,
  ...props
}: ButtonProps) => {
  const baseClasses =
    "inline-flex items-center rounded-md font-medium transition-all duration-200";
  const variantClasses = {
    primary:
      "bg-gradient-to-r from-red-600 to-orange-500 text-white hover:from-red-700 hover:to-orange-600 shadow-md hover:shadow-lg",
    secondary: "bg-gray-600 text-white hover:bg-gray-700",
    success: "bg-green-600 text-white hover:bg-green-700",
    danger: "bg-red-600 text-white hover:bg-red-700",
    warning: "bg-yellow-600 text-white hover:bg-yellow-700",
    outline:
      "border border-current bg-transparent hover:bg-gray-100 dark:hover:bg-gray-800",
  };
  const sizeClasses = {
    small: "text-sm px-3 py-1.5",
    medium: "text-base px-4 py-2",
    large: "text-lg px-6 py-3",
  };
  const classes = `${baseClasses} ${variantClasses[variant]} ${sizeClasses[size]}`;

  if (href) {
    return (
      <Link href={href} className={classes}>
        {children}
      </Link>
    );
  } else {
    return (
      <button className={classes} {...props}>
        {children}
      </button>
    );
  }
};

export default Button;
