//-----------------------------------------------------------------------------------------
// <copyright file="OperationProviderFactory.cs" company="Tasty Codes">
//     Copyright (c) 2012 Chad Burggraf.
// </copyright>
//-----------------------------------------------------------------------------------------

namespace Raspy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides creation and fetching services for <see cref="IOperationProvider"/>s.
    /// </summary>
    public sealed class OperationProviderFactory
    {
        private static OperationProviderFactory defaultInstance = new OperationProviderFactory();
        private Dictionary<char, IOperationProvider> providerCache = new Dictionary<char, IOperationProvider>();
        private IOperationProvider[] providers;

        /// <summary>
        /// Initializes a new instance of the OperationProviderFactory class.
        /// </summary>
        public OperationProviderFactory()
        {
            this.providers = new IOperationProvider[] 
            {
                new ArithmeticOperationProvider()
            };
        }

        /// <summary>
        /// Initializes a new instance of the OperationProviderFactory class.
        /// </summary>
        /// <param name="providers">The custom <see cref="IOperationProvider"/> collection to use.</param>
        public OperationProviderFactory(IEnumerable<IOperationProvider> providers)
        {
            this.providers = (providers ?? new IOperationProvider[0]).ToArray();

            if (this.providers.Length == 0)
            {
                throw new ArgumentException("The providers collection must contain at least one IOperationProvider implementation.", "providers");
            }
        }

        /// <summary>
        /// Gets the default <see cref="OperationProviderFactory"/> instance.
        /// </summary>
        public static OperationProviderFactory DefaultInstance
        {
            get { return defaultInstance; }
        }

        /// <summary>
        /// Gets the <see cref="IOperationProvider"/> to use when creating or executing the operator for the given symbol.
        /// </summary>
        /// <param name="symbol">The symbol to get the <see cref="IOperationProvider"/> for.</param>
        /// <returns>An <see cref="IOperationProvider"/>, or null if none was found for the given symbol.</returns>
        public IOperationProvider GetProvider(char symbol)
        {
            IOperationProvider provider = null;
            bool containsKey = false;

            lock (this.providerCache)
            {
                if (this.providerCache.ContainsKey(symbol))
                {
                    provider = this.providerCache[symbol];
                    containsKey = true;
                }
                else
                {
                    containsKey = false;
                }
            }

            if (!containsKey)
            {
                provider = (from p in this.providers
                            where p.CanCreateOperator(symbol)
                            select p).FirstOrDefault();

                lock (this.providerCache)
                {
                    this.providerCache[symbol] = provider;
                }
            }

            return provider;
        }
    }
}
