# Uniswap API

This API is developed by [LoanScan](https://loanscan.io/) Team for [Uniswap Protocol](https://uniswap.io/).

The API is developed according to the [documentation](https://docs.google.com/document/d/1J7tKlnjaur3CP2MEzr79aXJtwHC8x9Sesa0k07RIkLk/edit#heading=h.d926pckxcyyf).

The data provided by API is real-time.

It scans:
1. Factory contact for getting data about the new exchanges;
2. Each of known exchanges contract for getting data about the new events.

There are two infrastructure dependencies:
1. The Ethereum Node (correct url should be added to the config);
2. The MongoDB (correct connection string should be added to the config).

In case of any questions or problems please feel free to add new issue or [contact](mailto:contact@loanscan.io) LoanScan Team.
