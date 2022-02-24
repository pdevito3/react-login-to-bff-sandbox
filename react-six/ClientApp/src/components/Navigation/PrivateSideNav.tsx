import React from 'react';

import { Fragment, useState } from 'react';
import { Dialog, Transition } from '@headlessui/react';
import {
	CalendarIcon,
	ChartBarIcon,
	FolderIcon,
	HomeIcon,
	InboxIcon,
	MenuIcon,
	UsersIcon,
	CakeIcon,
	XIcon,
} from '@heroicons/react/outline';
import clsx from 'clsx';
import { NavLink } from 'react-router-dom';

const navigation = [
	{ name: 'Home', href: '/', icon: HomeIcon, current: true },
	{ name: 'Recipes', href: '/recipes', icon: CakeIcon, current: false },
	// { name: 'Team', href: '#', icon: UsersIcon, current: false },
	// { name: 'Projects', href: '#', icon: FolderIcon, current: false },
	// { name: 'Calendar', href: '#', icon: CalendarIcon, current: false },
	// { name: 'Documents', href: '#', icon: InboxIcon, current: false },
	// { name: 'Reports', href: '#', icon: ChartBarIcon, current: false },
];

export default function PrivateSideNav() {
	const [sidebarOpen, setSidebarOpen] = useState(false);

	return (
		<>
			<div>
				<Transition.Root show={sidebarOpen} as={Fragment}>
					<Dialog as="div" className="fixed inset-0 z-40 flex md:hidden" onClose={setSidebarOpen}>
						<Transition.Child
							as={Fragment}
							enter="transition-opacity ease-linear duration-[400ms]"
							enterFrom="opacity-0"
							enterTo="opacity-100"
							leave="transition-opacity ease-linear duration-[400ms]"
							leaveFrom="opacity-100"
							leaveTo="opacity-0"
						>
							<Dialog.Overlay className="fixed inset-0 bg-gray-700 bg-opacity-75" />
						</Transition.Child>
						<Transition.Child
							as={Fragment}
							enter="transition ease-in-out duration-[400ms] transform"
							enterFrom="translate-y-full"
							enterTo="-translate-y-0"
							leave="transition ease-in-out duration-[400ms] transform"
							leaveFrom="-translate-y-0"
							leaveTo="translate-y-full"
						>
							<div className="absolute bottom-0 w-full px-2 h-1/2">
								<div className="relative flex flex-col flex-1 w-full h-full bg-gray-800 rounded-t-xl">
									<Transition.Child
										as={Fragment}
										enter="ease-in-out duration-[400ms]"
										enterFrom="opacity-0"
										enterTo="opacity-100"
										leave="ease-in-out duration-[400ms]"
										leaveFrom="opacity-100"
										leaveTo="opacity-0"
									>
										<div className="absolute top-0 right-0 mr-2 -mt-12">
											<button
												type="button"
												className="flex items-center justify-center w-10 h-10 ml-1 rounded-full focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white"
												onClick={() => setSidebarOpen(false)}
											>
												<span className="sr-only">Close sidebar</span>
												<XIcon className="w-6 h-6 text-white" aria-hidden="true" />
											</button>
										</div>
									</Transition.Child>
									<div className="flex-1 h-0 pt-5 pb-4 overflow-y-auto">
										<div className="flex items-center flex-shrink-0 px-4">
											<img
												className="w-auto h-8"
												src="https://tailwindui.com/img/logos/workflow-logo-indigo-300-mark-white-text.svg"
												alt="Workflow"
											/>
										</div>
										<nav className="mt-5">
											{navigation.map((item) => (
												<NavLink
													key={item.name}
													to={item.href}
													className={({ isActive }) => clsx(
														isActive
															? 'bg-gray-900 text-white border-l-4 border-white'
															: 'text-white hover:bg-gray-700 hover:bg-opacity-75',
														'group flex items-center p-4 text-base font-medium'
													)}
												>
													<item.icon
														className="flex-shrink-0 w-6 h-6 mr-4 text-gray-400"
														aria-hidden="true"
													/>
													{item.name}
												</NavLink>
											))}
										</nav>
									</div>
								</div>
							</div>
						</Transition.Child>
						<div className="flex-shrink-0 w-14" aria-hidden="true">
							{/* Force sidebar to shrink to fit close icon */}
						</div>
					</Dialog>
				</Transition.Root>

				{/* Static sidebar for desktop */}
				<div className="hidden h-full md:flex md:w-64 md:flex-col">
					{/* Sidebar component, swap this element with another sidebar if you like */}
					<div className="flex flex-col flex-1 min-h-0 bg-gray-800">
						<div className="flex items-center flex-shrink-0 h-16 px-4 bg-gray-900 shadow-lg">
							<img
								className="w-auto h-8"
								src="https://tailwindui.com/img/logos/workflow-logo-indigo-500-mark-white-text.svg"
								alt="Workflow"
							/>
						</div>
						<div className="flex flex-col flex-1 py-2 overflow-y-auto">
							<nav className="flex-1 mt-6 space-y-1">
								{navigation.map((item) => (
									<NavLink
										key={item.name}
										to={item.href}
										className={({ isActive }) => clsx(
											isActive
												? 'bg-gray-900 text-white border-l-4 border-white'
												: 'text-white hover:bg-gray-700 hover:bg-opacity-75',
											'group flex items-center px-3 py-3 text-sm font-medium'
										)}
									>
										<item.icon
											className="flex-shrink-0 w-6 h-6 mr-3 text-gray-500"
											aria-hidden="true"
										/>
										{item.name}
									</NavLink>
								))}
							</nav>
						</div>
					</div>
				</div>
				<div className="flex flex-col flex-1 md:pl-64">
					<div className="absolute z-10 p-1 bg-gray-100 rounded-full bottom-4 right-4 sm:p-2 md:hidden">
						<button
							type="button"
							className="-ml-0.5 -mt-0.5 inline-flex h-12 w-12 items-center justify-center rounded-md text-gray-500 hover:text-gray-900 focus:outline-none focus:ring-2 focus:ring-inset focus:ring-gray-500"
							onClick={() => setSidebarOpen(true)}
						>
							<span className="sr-only">Open sidebar</span>
							<MenuIcon className="w-6 h-6 text-gray-900" aria-hidden="true" />
						</button>
					</div>
				</div>
			</div>
		</>
	);
}

export { PrivateSideNav };
